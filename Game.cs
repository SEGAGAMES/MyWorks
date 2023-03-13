using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace lb5
{
    internal class Game : GameWindow
    {
        List<Figura> objects = new List<Figura>();
        private Vector2 cursorPosition = new Vector2();
        Figura? chsFigura = null;
        int[,] a;
        int[,] b;
        List<List<int>> list;
        public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
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
            GL.ClearColor(0.0f, 1.0f, 0.0f, 1.0f);
            a = new int[5,5]
            {
                {0,1,0,1,0 },
                {0,0,1,0,0 },
                {0,0,0,0,1 },
                {0,0,0,0,1 },
                {0,0,0,0,0 },
            };
            b = new int[5, 6]
            {
                {-1,1,0,0,0,1 },
                {0,-1,-1,0,0,0},
                {0,1,1,1,0,0 },
                {1,0,0,-1,-1,-1 },
                {0,0,0,0,1,0 },
            };
            list = new List<List<int>>()
            {
                new List<int>() { 3, },
                new List<int>() { 0, 1, 2 },
                new List<int>() {},
                new List<int>() {0,2,4},
                new List<int>() {},
            };
            objects = Line.CreateList(b);
           // Line.DFS(0, a); // Это типо для другого какого-то задания
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            Line.Draw(b, objects);
            SwapBuffers();
        }
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            foreach (var obj in objects) 
                if (obj.IsPointIn(cursorPosition))
                {
                    chsFigura = obj;
                    chsFigura.Touch(cursorPosition);
                    break;
                }
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (chsFigura != null)
                chsFigura = null;
        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);
            cursorPosition.X = 2 * e.X / ClientSize.X - 1f;
            cursorPosition.Y = -(2 * e.Y / ClientSize.Y - 1f);
            if (chsFigura != null)
                chsFigura.Position = cursorPosition;
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0,0,e.Width,e.Height);
        }
    }
}
