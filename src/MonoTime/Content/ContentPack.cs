﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.ContentPack
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DuckGame
{
    public class ContentPack
    {
        public static long kTotalKilobytesAllocated;
        public long kilobytesPreAllocated;
        public List<string> levels = new List<string>();
        protected Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
        protected Dictionary<string, SoundEffect> _sounds = new Dictionary<string, SoundEffect>();
        protected Dictionary<string, Song> _songs = new Dictionary<string, Song>();
        protected ModConfiguration _modConfig;
        public static ContentPack currentPreloadPack;
        private long _beginCalculatingAllocatedBytes;

        public ContentPack(ModConfiguration modConfiguration) => this._modConfig = modConfiguration;

        public void ImportAsset(string path, byte[] data)
        {
            try
            {
                string str = path.Substring(0, path.Length - 4);
                if (path.EndsWith(".png"))
                {
                    Texture2D texture2D = TextureConverter.LoadPNGWithPinkAwesomeness(DuckGame.Graphics.device, new Bitmap((Stream)new MemoryStream(data)), true);
                    this._textures[str] = texture2D;
                    Content.textures[str] = (Tex2D)texture2D;
                }
                else
                {
                    if (!path.EndsWith(".wav"))
                        return;
                    SoundEffect pEffect = SoundEffect.FromStream((Stream)new MemoryStream(data));
                    if (pEffect != null)
                    {
                        pEffect.file = path;
                        this._sounds[str] = pEffect;
                        SFX.RegisterSound(str, pEffect);
                    }
                    else
                        DevConsole.Log(DCSection.General, "|DGRED|Failed to load sound effect! (" + path + ")");
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Called when the mod is loaded to preload content. This is only called if preload is set to true.
        /// </summary>
        public virtual void PreloadContent() => this.PreloadContentPaths();

        /// <summary>
        /// Called when the mod is loaded to preload the paths to all content. Does not actually load content, and is only called if PreloadContent is disabled (looks like that's a lie, this function loads the content).
        /// </summary>
        public virtual void PreloadContentPaths()
        {
            List<string> files = Content.GetFiles<Texture2D>(this._modConfig.contentDirectory);
            int length = this._modConfig.contentDirectory.Length;
            foreach (string file in files)
            {
                Texture2D texture2D = ContentPack.LoadTexture2DInternal(file);
                string key = file.Substring(0, file.Length - 4);
                this._textures[key] = texture2D;
                Content.textures[key] = (Tex2D)texture2D;
            }
            foreach (string file in Content.GetFiles<SoundEffect>(this._modConfig.contentDirectory))
            {
                string s = file;
                MonoMain.currentActionQueue.Enqueue(new LoadingAction((Action)(() =>
               {
                   ContentPack.currentPreloadPack = this;
                   SoundEffect pEffect = this.LoadSoundInternal(s);
                   string str = s.Substring(0, s.Length - 4);
                   this._sounds[str] = pEffect;
                   SFX.RegisterSound(str, pEffect);
               })));
            }
            string str1 = this._modConfig.contentDirectory + "/Levels";
            if (DuckFile.DirectoryExists(str1))
                this.levels = Content.GetFiles<Level>(str1);
            MonoMain.currentActionQueue.Enqueue(new LoadingAction((Action)(() =>
           {
               ContentPack.currentPreloadPack = (ContentPack)null;
               if (this.kilobytesPreAllocated / 1000L <= 20L)
                   return;
               MonoMain.CalculateModMemoryOffendersList();
           })));
        }

        private static Texture2D LoadTexture2DInternal(string file, bool processPink = true)
        {
            try
            {
                return TextureConverter.LoadPNGWithPinkAwesomeness(DuckGame.Graphics.device, file, processPink);
            }
            catch (Exception ex)
            {
                throw new Exception("PNG Load Fail(" + Path.GetFileName(file) + "): " + ex.Message, ex);
            }
        }

        public static Texture2D LoadTexture2DFromStream(Stream data, bool processPink = true)
        {
            try
            {
                return TextureConverter.LoadPNGWithPinkAwesomeness(DuckGame.Graphics.device, data, processPink);
            }
            catch (Exception ex)
            {
                throw new Exception("PNG Load Fail: " + ex.Message, ex);
            }
        }

        public static PNGData LoadPNGDataFromStream(Stream data, bool processPink = true)
        {
            try
            {
                return TextureConverter.LoadPNGDataWithPinkAwesomeness(data, processPink);
            }
            catch (Exception ex)
            {
                throw new Exception("PNG Load Fail: " + ex.Message, ex);
            }
        }

        public static Texture2D LoadTexture2D(string name, bool processPink = true)
        {
            Texture2D texture2D = (Texture2D)null;
            if (!name.EndsWith(".png"))
                name += ".png";
            if (System.IO.File.Exists(name))
            {
                try
                {
                    texture2D = ContentPack.LoadTexture2DInternal(name);
                }
                catch (Exception ex)
                {
                    DevConsole.Log(DCSection.General, "LoadTexure2D Error (" + name + "): " + ex.Message);
                }
            }
            return texture2D;
        }

        internal SoundEffect LoadSoundInternal(string file)
        {
            SoundEffect soundEffect = (SoundEffect)null;
            try
            {
                soundEffect = new FileInfo(file).Length <= 5000000L ? SoundEffect.FromStream((Stream)new MemoryStream(System.IO.File.ReadAllBytes(file)), Path.GetExtension(file)) : SoundEffect.FromStream((Stream)new FileStream(file, FileMode.Open), Path.GetExtension(file));
                if (soundEffect != null)
                    soundEffect.file = file;
            }
            catch (Exception ex)
            {
            }
            return soundEffect;
        }

        internal SoundEffect LoadSoundEffect(string name)
        {
            SoundEffect soundEffect = (SoundEffect)null;
            if (Path.GetExtension(name) == "")
                name += ".wav";
            if (System.IO.File.Exists(name))
                soundEffect = this.LoadSoundInternal(name);
            return soundEffect;
        }

        internal Song LoadSongInternal(string file)
        {
            Song song = (Song)null;
            try
            {
                MemoryStream dat = OggSong.Load(file, false);
                if (dat != null)
                    song = new Song(dat, file);
            }
            catch
            {
            }
            return song;
        }

        internal Song LoadSong(string name)
        {
            Song song = (Song)null;
            if (!name.EndsWith(".ogg"))
                name += ".ogg";
            if (System.IO.File.Exists(name))
                song = this.LoadSongInternal(name);
            return song;
        }

        /// <summary>
        /// Loads content from the content pack. Currently supports Texture2D(png) and SoundEffect(wav) in
        /// "mySound" "customSounds/mySound" path format. You should usually use Content.Load&lt;&gt;().
        /// </summary>
        public virtual T Load<T>(string name)
        {
            if (typeof(T) == typeof(Texture2D))
            {
                Texture2D texture2D1 = (Texture2D)null;
                if (this._textures.TryGetValue(name, out texture2D1))
                    return (T)(object)texture2D1;
                Texture2D texture2D2 = ContentPack.LoadTexture2D(name, this._modConfig == null || this._modConfig.processPinkTransparency);
                this._textures[name] = texture2D2;
                return (T)(object)texture2D2;
            }
            if (typeof(T) == typeof(SoundEffect))
            {
                SoundEffect soundEffect1 = (SoundEffect)null;
                if (this._sounds.TryGetValue(name, out soundEffect1))
                    return (T)(object)soundEffect1;
                SoundEffect soundEffect2 = this.LoadSoundEffect(name);
                this._sounds[name] = soundEffect2;
                return (T)(object)soundEffect2;
            }
            if (!(typeof(T) == typeof(Song)))
                return default(T);
            Song song1 = (Song)null;
            if (this._songs.TryGetValue(name, out song1))
                return (T)(object)song1;
            Song song2 = this.LoadSong(name);
            this._songs[name] = song2;
            return (T)(object)song2;
        }
    }
}
