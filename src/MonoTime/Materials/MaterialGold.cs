﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.MaterialGold
//removed for regex reasons Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using Microsoft.Xna.Framework.Graphics;

namespace DuckGame
{
    public class MaterialGold : Material  //todo make non sprite batch version, works with Spriteatlas
    {
        private Tex2D _goldTexture;
        private Thing _thing;
        private MTEffect _spritebatcheffect;
        private MTEffect _baseeffect;
        public MaterialGold(Thing t)
        {
            spsupport = true;
            _baseeffect = Content.Load<MTEffect>("Shaders/gold");
            _spritebatcheffect = Content.Load<MTEffect>("Shaders/sbgold");
            _goldTexture = Content.Load<Tex2D>("bigGold");
            _effect = _baseeffect;
            _thing = t;
        }
        public override void Apply()
        {
            if (this.batchItem != null && this.batchItem.NormalTexture != null && DuckGame.Content.offests.ContainsKey("bigGold") && DuckGame.Content.offests.ContainsKey(this.batchItem.NormalTexture.Namebase))
            {
                _effect = _spritebatcheffect;
                Microsoft.Xna.Framework.Rectangle r = DuckGame.Content.offests[this.batchItem.NormalTexture.Namebase];
                Microsoft.Xna.Framework.Rectangle r2 = DuckGame.Content.offests["bigGold"];
                //bigGold
                SetValue("width", this.batchItem.NormalTexture.frameWidth / (float)this.batchItem.NormalTexture.width);
                SetValue("height", this.batchItem.NormalTexture.frameHeight / (float)this.batchItem.NormalTexture.height);
                SetValue("xpos", _thing.x);
                SetValue("ypos", _thing.y);

                SetValue("sasize", new Vec2(Content.Thick.width, Content.Thick.height));
                SetValue("xoffset", r.X / (float)Content.Thick.width);
                SetValue("yoffset", r.Y / (float)Content.Thick.height);
                SetValue("spritesizex", r.Width / (float)Content.Thick.width);
                SetValue("spritesizey", r.Height / (float)Content.Thick.height);
                SetValue("goldxoffset", r2.X);
                SetValue("goldyoffset", r2.Y);
                SetValue("goldsizex", r2.Width);

                SetValue("goldsizey", r2.Height);
            }
            else if (Graphics.device.Textures[0] != null)
            {
                _effect = _baseeffect;
                Tex2D texture = Graphics.device.Textures[0] as Texture2D;
                this.SetValue("width", texture.frameWidth / (float)texture.width);
                this.SetValue("height", texture.frameHeight / (float)texture.height);
                this.SetValue("xpos", this._thing.x);
                this.SetValue("ypos", this._thing.y);
            }
            DuckGame.Graphics.device.Textures[1] = (Texture2D)_goldTexture;
            DuckGame.Graphics.device.SamplerStates[1] = SamplerState.PointWrap;
            foreach (EffectPass pass in effect.effect.CurrentTechnique.Passes)
                pass.Apply();
        }
    }
}
