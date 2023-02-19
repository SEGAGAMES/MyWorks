using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameWindowSettings gameWindowSettings = GameWindowSettings.Default;
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Title = "Laboratornaya rabota 5",
                Size = (600, 900),
                Flags = OpenTK.Windowing.Common.ContextFlags.Default,
                Profile = ContextProfile.Compatability,
            };
            MyGame game = new MyGame(gameWindowSettings, nativeWindowSettings);
            game.Run();
        }
    }
}