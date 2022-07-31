﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.EnergyBlocker
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    public class EnergyBlocker : MaterialThing
    {
        private OldEnergyScimi _parent;

        public EnergyBlocker(OldEnergyScimi pParent)
          : base(0f, 0f)
        {
            this.thickness = 100f;
            this._editorCanModify = false;
            this.visible = false;
            this._parent = pParent;
            this.weight = 0.01f;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            if (!this._solid)
                return false;
            if (this._parent != null)
                this._parent.Shing();
            if (!(bullet.ammo is ATLaser))
                return base.Hit(bullet, hitPos);
            bullet.reboundOnce = true;
            return true;
        }
    }
}
