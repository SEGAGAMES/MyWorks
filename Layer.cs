using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace НейросетьВ2
{
    public abstract class Layer
    {
        public int NeuronInLayer;
        public List<Neuron> Neurons = new List<Neuron>();

        public List<Neuron> GetNeurons()
        { 
            List<Neuron> neurons = new List<Neuron>();
            foreach (Neuron neuron in Neurons) neurons.Add(neuron);
            return neurons;
        }
        public void SetNeurons(List<Neuron> neurons)
        {
            foreach (Neuron neuron in neurons) Neurons.Add(neuron);
        }
        public Layer(List<Neuron> neurons)
        {
            NeuronInLayer = neurons.Count;
            SetNeurons(neurons);
        }
    }
}
