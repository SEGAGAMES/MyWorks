using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib1
{
    [Serializable]
    public class Planets
    {
        public Planets() { }
        public List<Planet> planetlist { get; set; } = new List<Planet>();
    }
}
