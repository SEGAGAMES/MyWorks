using OpenTK.Graphics.OpenGL;
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
    internal class Circle: Figura
    {
        float r;
        public Circle(float x, float y, float r, Vector3 color): base(color, new Vector2(x, y))
        {
            this.r = r;
        }
        public override void Draw()
        {
            DrawPolygon(Position, 40, r, color);
        }
        public override bool IsPointIn(Vector2 test)
        {
            return Math.Pow(test.X - Position.X, 2) + Math.Pow(test.Y - Position.Y, 2) < r * r;
        }
        private void DrawPolygon(Vector2 center, int n, double r, Vector3 color)
        {
            Vector2[] vertexs = new Vector2[n];
            for (int i = 0; i < n; i++)
            {
                vertexs[i] = new Vector2(
                   (float)(center.X + r * Math.Cos(i * 2 * Math.PI / n)),
                   (float)(center.Y + r * Math.Sin(i * 2 * Math.PI / n)));
            }
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(color);
            foreach (Vector2 v in vertexs)
            {
                GL.Vertex2(v.X, v.Y);
            }
            GL.End();
        }
    }
}
