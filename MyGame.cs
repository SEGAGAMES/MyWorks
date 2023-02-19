using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;

namespace Game
{
    internal class MyGame : GameWindow
    {
        public Catcher catcher = new Catcher(0, -0.9f, 0.5f, 0.1f, 0.0005f);
        public Cube cube = new Cube(0, 0.9f, 0.0005f);
        int score = 0;

        public MyGame(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.Escape))
                Close();
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color.Blue);
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            if (KeyboardState.IsKeyDown(Keys.Left))
                catcher.PositionX -= catcher.Speed;
            if (KeyboardState.IsKeyDown(Keys.Right))
                catcher.PositionX += catcher.Speed;
            //if (KeyboardState.IsKeyDown(Keys.Down))
            //{
            //    if (cube.Speed < 0.001)
            //        cube.Speed = cube.Speed + 0.0005f;
            //}
            //else cube.Speed = 0.0005f;
            cube.PositionY -= cube.Speed;
            catcher.DrawCather();
            cube.DrawCube();
            Check();
            SwapBuffers();
        }
        void Check()
        {
            if (cube.PositionY - 0.1f <= catcher.PositionY)
            {
                if (cube.PositionX + 0.1f > catcher.PositionX && cube.PositionX < catcher.PositionX + catcher.Weight)
                {
                    score++;
                    cube.Speed = - cube.Speed;
                    //cube.Respawn();
                    Console.WriteLine($"{score}");
                }
            }
            if (cube.PositionY > 1)
                cube.Speed = -cube.Speed;
            if (cube.PositionY < -0.9)
            {
                cube.Respawn();
            }
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }
        protected override void OnUnload()
        {
            base.OnUnload();
        }
    }
}

