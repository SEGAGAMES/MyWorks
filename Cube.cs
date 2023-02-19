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
    internal class Cube
    {
        float positionX;
        float positionY;
        float speed;

        public Cube(float positionX, float positionY, float speed)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.speed = speed;
        }

        public float PositionX { get => positionX; set => positionX = value; }
        public float PositionY { get => positionY; set => positionY = value; }
        public float Speed { get => speed; set => speed = value; }
        Random rnd = new Random();
        internal void Respawn()
        {
            if (rnd.Next(1,3) == 1)
                this.positionX = (float)rnd.Next(1, 11) / 11f;
            if (rnd.Next(1, 3) == 2)
                this.positionX = -(float)rnd.Next(1, 11) / 11f;
            this.PositionY = 0.9f;
        }
        internal void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Red);
            GL.Vertex2(this.PositionX, this.PositionY);
            GL.Vertex2(this.PositionX, this.PositionY - 0.1f);
            GL.Vertex2((this.PositionX + 0.1f), (this.PositionY - 0.1f));
            GL.Vertex2((this.PositionX + 0.1f), this.PositionY);
            GL.End();
        }
    }
}
