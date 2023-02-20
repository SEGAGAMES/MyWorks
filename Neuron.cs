using System;
using System.Collections.Generic;

namespace НейросетьВ2
{
    public class Neuron
    {
        public List<double> listWeightIn = new List<double>();
        public List<double> listIn = new List<double>();

        public double Output()
        {
            double summinput = 0;
            for (int i = 0; i < listWeightIn.Count; i++)
            {
                summinput = summinput + listWeightIn[i] * listIn[i];
            }
            return Sigmoid(summinput / listWeightIn.Count);
        }
        public double Sigmoid(double weight)
        {
            return 1 / (1 + Math.Pow(Math.E, -weight));
        }

        public void SetListWeights(List<double> weights)
        {
            listWeightIn.Clear();
            foreach (double weight in weights) listWeightIn.Add(weight);
        }

        public void SetListIn(List<double> inputs)
        {
            listIn.Clear();
            foreach (double input in inputs) listIn.Add(input);
        }
        
        public double GetListWeights(int i)
        {
            return listWeightIn[i];
        }

        public double GetListIn(int i)
        {
            return listIn[i];
        }

        public Neuron(List<double> inp, List<double> weights)
        {
            SetListIn(inp);
            SetListWeights(weights);
        }
        public Neuron(double inp)
        {
            listIn.Add(inp);
        }
        public Neuron(List<double> weights)
        {
            SetListWeights(weights);
        }
    }
}
