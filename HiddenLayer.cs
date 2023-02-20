using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace НейросетьВ2
{
    public class HiddenLayer:Layer
    {
        public HiddenLayer(List<Neuron> neurons): base(neurons)
        {
        }
        public double Sigmoid(double weight)
        {
            return 1 / (1 + Math.Pow(Math.E, -weight));
        }
        public List<double> Output()
        {
            List<double> output = new List<double>();
            foreach (Neuron neuron in Neurons) 
                output.Add(neuron.Output());
            return output;
        }
    }
}
