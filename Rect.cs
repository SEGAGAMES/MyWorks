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
    internal class Rect: Figura
    {
        float wigth, heighth;

        public Rect(float wigth, float heighth, float x, float y, Vector3 color):base(color, new Vector2(x,y) )
        {
            this.wigth = wigth;
            this.heighth = heighth;
        }
        public override void Draw()
        {
            GL.Color3(color);
            GL.Begin(PrimitiveType.Polygon);
            GL.Vertex2(Position.X - wigth / 2, Position.Y - heighth / 2);
            GL.Vertex2(Position.X + wigth / 2, Position.Y - heighth / 2);
            GL.Vertex2(Position.X + wigth / 2, Position.Y + heighth / 2);
            GL.Vertex2(Position.X - wigth / 2, Position.Y + heighth / 2);
            GL.End();
        }
        public override bool IsPointIn(Vector2 test)
        {
            return (Position.X - wigth / 2 < test.X &&
                test.X < Position.X + wigth / 2 &&
                Position.Y - heighth / 2 < test.Y &&
                test.Y < Position.Y + heighth / 2
                );
        }
    }
}
