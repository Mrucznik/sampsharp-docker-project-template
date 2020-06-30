using System;
using SampSharp.Core;

namespace Gamemode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new GameModeBuilder()
                .Use<GameMode>()
                .Run();
        }
    }
}