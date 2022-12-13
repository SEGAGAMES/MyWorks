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
    internal class Ring:Figura
    {
        float r;
        float r1;
        public Ring(float x, float y, float r,float r1, Vector3 color) : base(color, new Vector2(x, y))
        {
            this.r = r;
            this.r1 = r1;
        }
        public override void Draw()
        {
            DrawPolygon1(r1);
            DrawPolygon(r, color);
            
        }
        public override bool IsPointIn(Vector2 test)
        {
            return Math.Pow(test.X - Position.X, 2) + Math.Pow(test.Y - Position.Y, 2) < r * r;
        }
        private void DrawPolygon(double r, Vector3 color)
        {
            Vector2[] vertexs = new Vector2[40];
            for (int i = 0; i < 40; i++)
            {
                vertexs[i] = new Vector2(
                   (float)(Position.X + r * Math.Cos(i * 2 * Math.PI / 40)),
                   (float)(Position.Y + r * Math.Sin(i * 2 * Math.PI / 40)));
            }
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(color);
            foreach (Vector2 v in vertexs)
            {
                GL.Vertex2(v.X, v.Y);
            }
            GL.End();
        }
        private void DrawPolygon1(double r)
        {
            Vector2[] vertexs = new Vector2[40];
            for (int i = 0; i < 40; i++)
            {
                vertexs[i] = new Vector2(
                   (float)(Position.X + r * Math.Cos(i * 2 * Math.PI / 40)),
                   (float)(Position.Y + r * Math.Sin(i * 2 * Math.PI / 40)));
            }
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.One);
            GL.Enable(EnableCap.Blend);
            GL.Color4(0f, 0f, 0f, 0f);
            GL.Begin(PrimitiveType.Polygon);
            foreach (Vector2 v in vertexs)
            {
                GL.Vertex2(v.X, v.Y);
            }
            GL.End();
            GL.Disable(EnableCap.Blend);
        }
    }
}
