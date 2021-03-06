﻿using System;

namespace mono_house_defense
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new HouseDefenseGame())
                game.Run();
        }
    }
#endif
}
