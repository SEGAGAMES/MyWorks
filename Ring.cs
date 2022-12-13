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
        float rLow;
        float rBiggest;
        public Ring(float x, float y, float rlow,float r1, Vector3 color) : base(color, new Vector2(x, y))
        {
            this.rLow = rlow;
            this.rBiggest = r1;
        }
        public override void Draw()
        {
            DrawPolygon2();
            
        }
        public override bool IsPointIn(Vector2 test)
        {
            return Math.Pow(test.X - Position.X, 2) + Math.Pow(test.Y - Position.Y, 2) > rLow * rLow && Math.Pow(test.X - Position.X, 2) + Math.Pow(test.Y - Position.Y, 2) < rBiggest * rBiggest;
        }
        private void DrawPolygon2()
        {
            int n = 10000;
            Vector2[] innerPoints = new Vector2[n];
            for (int i = 0; i < n; i++)
            {
                innerPoints[i] = new Vector2(
                   (float)(Position.X + rLow * Math.Cos(i * 2 * Math.PI / n)),
                   (float)(Position.Y + rLow * Math.Sin(i * 2 * Math.PI / n)));
            }
            Vector2[] outterPoints = new Vector2[n];
            for (int i = 0; i < n; i++)
            {
                outterPoints[i] = new Vector2(
                   (float)(Position.X + rBiggest * Math.Cos(i * 2 * Math.PI / n)),
                   (float)(Position.Y + rBiggest * Math.Sin(i * 2 * Math.PI / n)));
            }
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(color);
            for(int i = 0; i < n; i++)
            {
                GL.Vertex2(innerPoints[i].X, innerPoints[i].Y);
                GL.Vertex2(outterPoints[i].X, outterPoints[i].Y);
            }
            GL.End();
        }
    }
}
