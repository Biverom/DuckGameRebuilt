﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.OggSong
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System.IO;

namespace DuckGame
{
    public class OggSong
    {
        public static MemoryStream Load(string oggFile, bool localContent = true)
        {
            oggFile = oggFile.TrimStart('/');
            Stream input = !localContent ? (Stream)System.IO.File.OpenRead(oggFile) : DuckFile.OpenStream(oggFile);
            MemoryStream output = new MemoryStream();
            OggSong.CopyStream(input, (Stream)output);
            input.Close();
            return output;
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[16384];
            int count;
            while ((count = input.Read(buffer, 0, buffer.Length)) > 0)
                output.Write(buffer, 0, count);
        }
    }
}
