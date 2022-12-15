using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ВолкиП
{
    internal class Wolf:Animal
    {
        bool life = true;
        public bool Life { get => life; set { life = value; Position.Text = string.Empty;  } }
        public Wolf(Button spawnPosition):base(spawnPosition)
        {
                Text = "W";
                Position.Text = Text;
        }
        public override void Move(Button newPosition)
        {
            if (life)
            {
                Position.Text = string.Empty;
                Position = newPosition;
                Position.Text = Text;
            }
        }
    }
}
