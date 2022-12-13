using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Runtime;

namespace lb5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameWindowSettings gameWindowSettings = GameWindowSettings.Default;
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Title = "Laboratornaya rabota 5",
                Size = (600, 600),
                Flags = OpenTK.Windowing.Common.ContextFlags.Default,
                Profile = ContextProfile.Compatability,
            };
            Game game = new Game(gameWindowSettings, nativeWindowSettings);
            game.Run();
        }
    }
}
