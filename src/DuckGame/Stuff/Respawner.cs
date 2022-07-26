﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.Respawner
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System;

namespace DuckGame
{
    [BaggedProperty("isInDemo", true)]
    public class Respawner : Thing, IDrawToDifferentLayers
    {
        private SpriteMap _sprite;
        private float _animate;
        private float _noiseOffset;

        public Respawner(float xpos, float ypos)
          : base(xpos, ypos)
        {
            this._sprite = new SpriteMap("respawner", 18, 10);
            this.graphic = (Sprite)this._sprite;
            this.center = new Vec2(9f, 5f);
            this.collisionOffset = new Vec2(-8f, -4f);
            this.collisionSize = new Vec2(16f, 4f);
            this.hugWalls = WallHug.Floor;
            this.layer = Layer.Blocks;
            this.depth = (Depth)0.8f;
            this._animate = Rando.Float(100f);
            this.editorTooltip = "";
        }

        public override void Update() => base.Update();

        public void OnDrawLayer(Layer pLayer)
        {
            if (pLayer != Layer.Game)
                return;
            this._animate += 0.05f;
            double y = (double)this.y;
            int num1 = 6;
            for (int index = 0; index < num1; ++index)
            {
                Vec2 p1 = new Vec2(this.x - 6f, (float)((double)this.y - (double)index * 4.0 - (double)this._animate % 1.0 * 4.0));
                float num2 = (float)(1.0 - ((double)this.y - (double)p1.y) / 24.0);
                float width = num2 * 3f;
                p1.y += width / 2f;
                Graphics.DrawLine(p1, p1 + new Vec2(12f, 0.0f), Colors.DGBlue * (num2 * 0.8f), width, - 0.75f);
            }
            Vec2 vec2_1 = new Vec2(7f, 8f);
            Vec2 vec2_2 = this.position + new Vec2(-7f, -24f);
            for (int index = 0; (double)index < (double)vec2_1.x * (double)vec2_1.y; ++index)
            {
                Vec2 vec2_3 = new Vec2((float)(int)((double)index % (double)vec2_1.x), (float)(int)((double)index / (double)vec2_1.y));
                float num3 = (float)(((double)Noise.Generate(vec2_3.x * 32f, 0.0f) + 1.0) / 2.0 * 1.5 + 0.100000001490116);
                float num4 = this._animate * 0.1f - (float)(int)((double)this._animate * (double)num3 / 1.0);
                float num5 = Noise.Generate(vec2_3.x + 100f, (float)(((double)vec2_3.y + 100.0 - (double)num4) * 0.5));
                if ((double)num5 > 0.25)
                {
                    vec2_3.y -= (float)((double)this._animate * (double)num3 % 1.0);
                    float num6 = 1f - Math.Abs((float)(((double)vec2_1.x / 2.0 - (double)vec2_3.x) / (double)vec2_1.x * 2.0));
                    float num7 = (float)(((double)num5 - 0.25) / 0.75) * num6 * Math.Max(0.0f, Math.Min((float)(((double)vec2_3.y / (double)vec2_1.y - 0.100000001490116) * 2.0), 1f));
                    vec2_3 *= 2f;
                    vec2_3.y *= 2f;
                    Graphics.DrawRect(vec2_3 + vec2_2, vec2_3 + vec2_2 + new Vec2(1f, 1f), Color.White * num7, - 0.5f);
                }
            }
        }
    }
}
