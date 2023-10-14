﻿using AddedContent.Firebreak;

namespace DuckGame
{

    public static partial class DevConsoleCommands
    {
        [Marker.DevConsoleCommand(Description = "Visualizes the outer bounds of the current map", IsCheat = true)]
        public static bool ShowBounds()
        {
            return DevConsole.debugBounds ^= true;
        }
    }
}