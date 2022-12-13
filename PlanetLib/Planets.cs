using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetLib
{
    [Serializable]
    public class Planets
    {
        public Planets() { }
        public Planets(List<Planet> ps)
        {
            foreach(Planet p in ps)
                planetlist.Add(p);
        }
        public List<Planet> planetlist { get; set; } = new List<Planet>();
    }
}
