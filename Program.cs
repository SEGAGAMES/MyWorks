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
                Title = "Лабораторная работа №3",
                Size = (1200, 1200),
                Flags = OpenTK.Windowing.Common.ContextFlags.Default,
                Profile = ContextProfile.Compatability,
            };
            Game game = new Game(gameWindowSettings, nativeWindowSettings);
            Console.WriteLine("Синий - выходит, красный заходит");
            Console.WriteLine("Черый - двустороняя");
            game.RenderFrequency = 60;
            game.Run();
        }
    }
}
