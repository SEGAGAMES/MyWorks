using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ВолкиП
{
    internal abstract class Animal
    {
        protected Animal(Button position)
        {
            Position = position;
        }

        internal Button Position { get; set; }
        internal string Text { get; set; }

        public abstract void Move(Button newPos);
    }
}
