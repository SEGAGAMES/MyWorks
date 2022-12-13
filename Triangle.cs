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
    internal class Triangle : Figura
    {
        float length;

        public Triangle(float l, float x, float y, Vector3 color):base(color, new Vector2(x,y))
        {
            length = l;
        }
        public override void Draw()
        {
            GL.Color3(color);
            GL.Begin(PrimitiveType.Polygon);
            GL.Vertex2(Position.X + length / 2, Position.Y);
            GL.Vertex2(Position.X + length / 2, Position.Y + length / 2);
            GL.Vertex2(Position.X, Position.Y);
            GL.End();
        }
        public override bool IsPointIn(Vector2 test)
        {
            return test.X > Position.X && test.Y > Position.Y && test.X < Position.X + length/2 && test.Y < Position.Y + length / 2;
        }
    }
}
