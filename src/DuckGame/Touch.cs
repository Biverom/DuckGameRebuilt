﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.Touch
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

using System;

namespace DuckGame
{
    public class Touch
    {
        public static readonly Touch None = new Touch();
        public InputState state;
        public ulong touchFrame;
        public TSData data;
        public bool tap;
        public bool canBeDrag = true;
        public Vec2 originalPosition;

        public bool drag => this.canBeDrag && this.data != null && (double)(this.data.touchXY - this.originalPosition).length > 25.0;

        public Vec2 positionCamera => this.data == null ? Vec2.Zero : this.Transform(Level.current.camera);

        public Vec2 positionHUD => this.data == null ? Vec2.Zero : this.Transform(Layer.HUD.camera);

        public void SetData(TSData pData)
        {
            if (this.data == null && pData != null)
                this.originalPosition = pData.touchXY;
            this.data = pData;
        }

        public Vec2 Transform(Camera pCamera) => this.data != null ? pCamera.transformScreenVector(this.data.touchXY) : Vec2.Zero;

        public Vec2 TransformGrid(Camera pCamera, float pCellSize)
        {
            Vec2 vec2_1 = new Vec2(-1f, -1f);
            Vec2 vec2_2 = this.Transform(pCamera);
            if (vec2_2 != new Vec2(-1f, -1f))
            {
                vec2_2.x = (float)Math.Round((double)vec2_2.x / (double)pCellSize) * pCellSize;
                vec2_2.y = (float)Math.Round((double)vec2_2.y / (double)pCellSize) * pCellSize;
            }
            return vec2_2;
        }

        /// <summary>
        /// Does a collision check between the current touch and a rectangle. pCamera tells
        /// the function which camera space to transform the touch into
        /// </summary>
        /// <param name="pRect">Rectangle to collide with</param>
        /// <param name="pCamera">Camera coordinate space to transform touch into</param>
        /// <returns></returns>
        public bool Check(Rectangle pRect, Camera pCamera = null)
        {
            if (this.data == null)
                return false;
            if (pCamera == null)
                pCamera = Level.current.camera;
            return Collision.Point(this.Transform(pCamera), pRect);
        }

        /// <summary>
        /// Does a collision check between the current touch and a rectangle. pCamera tells
        /// the function which camera space to transform the touch into
        /// </summary>
        /// <param name="pRect">Rectangle to collide with</param>
        /// <param name="pCellSize">Grid snap to apply to touch point</param>
        /// <param name="pCamera">Camera coordinate space to transform touch into</param>
        /// <returns></returns>
        public bool CheckGrid(Rectangle pRect, float pCellSize, Camera pCamera = null)
        {
            if (this.data == null)
                return false;
            if (pCamera == null)
                pCamera = Level.current.camera;
            return Collision.Point(this.TransformGrid(pCamera, pCellSize), pRect);
        }
    }
}
