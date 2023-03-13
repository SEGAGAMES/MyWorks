using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb5
{
    internal abstract class Figura
    {
        private Vector3 color;

        protected Figura(Vector3 color, Vector2 position)
        {
            this.color = color;
            Position = position;
        }
        private Vector2 shift = new Vector2();
        public void Touch(Vector2 touch)
        {
            shift.X = touch.X - Position.X;
            shift.Y = touch.Y - Position.Y;
        }
        private Vector2 position;
        public Vector2 Position { get => position; set
            {
                position.X = value.X - shift.X;
                position.Y = value.Y - shift.Y;
            }
        }

        public Vector3 Color { get => color; set => color = value; }

        public abstract bool IsPointIn(Vector2 test);
        public abstract void Draw();
    }
}
