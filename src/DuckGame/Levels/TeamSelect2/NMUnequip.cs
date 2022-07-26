﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.NMUnequip
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    public class NMUnequip : NMEvent
    {
        public Duck duck;
        public Equipment equipment;

        public NMUnequip()
        {
        }

        public NMUnequip(Duck pDuck, Equipment pEquipment)
        {
            this.duck = pDuck;
            this.equipment = pEquipment;
        }

        public override void Activate()
        {
            if (this.duck != null && this.equipment != null)
                this.duck.Unequip(this.equipment, true);
            base.Activate();
        }
    }
}
