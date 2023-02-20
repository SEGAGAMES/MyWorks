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
using НейросетьВ2;

namespace Game
{
    internal class MyGame : GameWindow
    {
        static float speed = 0.05f;
        public Catcher catcher = new Catcher(0, -0.9f, 0.5f, 0.1f, speed);
        public Cube cube = new Cube(0, 0.9f, speed);
        int score = 0;
        int ideal = 0;
        double[] delta = new double[384];
        List<double> learn = new List<double>();
        double loss;
        Net net;
        Keys keyboardState;

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
            net = new Net(4, 64, 2, "o.txt");
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            List<double> input = new List<double>();
            input.Add(cube.PositionX);
            input.Add(cube.PositionY);
            input.Add(catcher.PositionX);
            input.Add(catcher.PositionY);
            double y1 = net.Ot(input)[0];
            double y2 = net.Ot(input)[1];
            loss += ((Math.Log(y1) + Math.Log(y2)) * ideal);
            if (y1 > y2)
                keyboardState =  Keys.Left;
            else 
                keyboardState = Keys.Right;
            if (keyboardState == Keys.Left)
                catcher.PositionX -= catcher.Speed;
            if (keyboardState ==Keys.Right)
                catcher.PositionX += catcher.Speed;
            net.Learning(input, loss, delta, out delta);
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
                    cube.Respawn();
                    Console.WriteLine($"{score}");
                    ideal = 1;
                }
            }
            if (cube.PositionY < -0.9)
            {
                ideal = 0;
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

