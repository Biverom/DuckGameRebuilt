﻿// Decompiled with JetBrains decompiler
// Type: DuckGame.NMChatMessage
// Assembly: DuckGame, Version=1.1.8175.33388, Culture=neutral, PublicKeyToken=null
// MVID: C907F20B-C12B-4773-9B1E-25290117C0E4
// Assembly location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.exe
// XML documentation location: D:\Program Files (x86)\Steam\steamapps\common\Duck Game\DuckGame.xml

namespace DuckGame
{
    public class NMChatMessage : NMDuckNetwork
    {
        public Profile profile;
        public ushort index;
        public string text = "";

        public NMChatMessage()
        {
        }

        public NMChatMessage(Profile pProfile, string t, ushort idx)
        {
            this.profile = pProfile;
            this.text = t;
            this.index = idx;
        }
    }
}
