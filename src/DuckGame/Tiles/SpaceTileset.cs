﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.SpaceTileset
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    [EditorGroup("Blocks")]
    [BaggedProperty("previewPriority", true)]
    public class SpaceTileset : AutoBlock
    {
        public SpaceTileset(float x, float y)
          : base(x, y, "spaceTileset")
        {
            this._editorName = "Space";
            this.physicsMaterial = PhysicsMaterial.Metal;
            this.verticalWidth = 10f;
            this.verticalWidthThick = 15f;
            this.horizontalHeight = 15f;
        }
    }
}
