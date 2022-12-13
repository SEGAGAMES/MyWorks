using System;
using System.Collections.Generic;
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

            objects.Add(new Rect(0.4f, 0.3f, -0.5f, -0.5f, new Vector3(1f, 0.57f, 0f)));
            objects.Add(new Rect(0.4f, 0.4f, -0.5f, -0.0f, new Vector3(1f, 0.76f, 0f)));
            objects.Add(new Rect(0.3f, 0.4f, -0.5f, 0.5f, new Vector3(1f, 0.24f, 0f)));
           // objects.Add(new Circle(0.5f, -0.5f, 0.15f, new Vector3(1f, 0.57f, 0f)));
            objects.Add(new Circle(0.5f, -0.0f, 0.2f, new Vector3(1f, 0.76f, 0f)));
            objects.Add(new Circle(0.5f, 0.5f, 0.15f, new Vector3(1f, 0.24f, 0f)));
            objects.Add(new Triangle(0.5f, 0f, 0f, new Vector3(1f, 0.57f, 0f)));
            objects.Add(new Ring(0.5f, -0.5f, 0.15f, 0.1f, new Vector3(1f, 0.76f, 0f)));

        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //GL.Begin(PrimitiveType.Quads);
            //GL.Color3(0.0f,0.5f, 1.0f); GL.Vertex2(0.0f, 0.0f);
            //GL.Color3(0.5f, 0.0f, 0.5f); GL.Vertex2(0.5f, 0f);
            //GL.Color3(1f, 0f, 0f); GL.Vertex2(0.5f, 0.5f);
            //GL.Color3(0.5f, 0f, 0.5f); GL.Vertex2(0f, 0.5f);
            //GL.End();

            //GL.LineWidth(3f);
            //Vector3 color = new Vector3(1f, 0.57f, 0f);
            //int n = 10;
            //double r = 0.4;
            //Vector2 center = new Vector2(0f, 0f);
            //Vector2[] vertexes = new Vector2[n];
            //for (int i = 0; i < n; i++)
            //{
            //    vertexes[i] = new Vector2(
            //        (float)(center.X + r * Math.Cos(i * 2 * Math.PI / n)),
            //        (float)(center.Y + r * Math.Sin(i * 2 * Math.PI / n)));
            //}
            //DrawPolygon(vertexes, color);
            //DrawPolygon2(new Vector2(0f, 0f), 15, 0.2, new Vector3(1f, 0f, 1f));

            foreach (var obj in objects) obj.Draw();

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
            for (int i = objects.Count-1; i > -1; i--)
            {
                if (objects[i].IsPointIn(cursorPosition))
                {
                    chsFigura = objects[i];
                    chsFigura.Touch(cursorPosition);
                    objects.Remove(chsFigura);
                    objects.Add(chsFigura);
                    break;
                }
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
        protected override void OnUnload()
        {
            base.OnUnload();
        }
        //private void DrawPolygon(Vector2[] vertexs, Vector3 color)
        //{
        //    GL.Begin(PrimitiveType.Polygon);
        //    GL.Color3(color);
        //    foreach (Vector2 v in vertexs)
        //    {
        //        GL.Vertex2(v.X, v.Y);
        //    }
        //    GL.End();
        //}
        //private void DrawPolygon2(Vector2 center, int n, double r, Vector3 color)
        //{
        //    Vector2[] vertexs = new Vector2[n];
        //    for (int i = 0; i < n; i++)
        //    {
        //        vertexs[i] = new Vector2(
        //           (float)(center.X + r * Math.Cos(i * 2 * Math.PI / n)),
        //           (float)(center.Y + r * Math.Sin(i * 2 * Math.PI / n)));
        //    }
        //    GL.Begin(PrimitiveType.Polygon);
        //    GL.Color3(color);
        //    foreach (Vector2 v in vertexs)
        //    {
        //        GL.Vertex2(v.X, v.Y);
        //    }
        //    GL.End();
        //}
    }
}
