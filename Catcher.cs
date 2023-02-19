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
    internal class Catcher
    {
        float positionX;
        float positionY;
        float weight;
        float height;
        float speed;

        public Catcher(float positionX, float positionY, float weight, float height, float speed)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.weight = weight;
            this.height = height;
            this.speed = speed;
        }

        public float PositionX { get => positionX; set => positionX = value; }
        public float PositionY { get => positionY; set => positionY = value; }
        public float Weight { get => weight; set => weight = value; }
        public float Height { get => height; set => height = value; }
        public float Speed { get => speed; set => speed = value; }
        internal void DrawCather()
        {
            if (this.PositionX + this.Weight > 1)
                this.PositionX = 1 - this.Weight;
            if (this.PositionX < -1)
                this.PositionX = -1;
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Yellow);
            GL.Vertex2(this.PositionX, this.PositionY);
            GL.Vertex2(this.PositionX, this.PositionY - this.Height);
            GL.Vertex2((this.PositionX + this.Weight), (this.PositionY - this.Height));
            GL.Vertex2((this.PositionX + this.Weight), this.PositionY);
            GL.End();
        }
    }
}
