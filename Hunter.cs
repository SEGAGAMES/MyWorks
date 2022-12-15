using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ВолкиП
{
    internal class Hunter: Animal
    {
        public Hunter(Button home): base(home) 
        {
            Text = "O";
            Position.Text = Text;
        }
        public override void Move(Button newPosition)
        {
                Position.Text = string.Empty;
                Position = newPosition;
                Position.Text = Text;
        }
    }
}
