﻿namespace DuckGame
{

    public static partial class DevConsoleCommands
    {
        [DevConsoleCommand(Description = "Toggles QWOP mode, similar to the modifier of the same name", IsCheat = true)]
        public static bool QwopMode()
        {
            return DevConsole.qwopMode ^= true;
        }
    }
}