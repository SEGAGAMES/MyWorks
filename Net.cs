using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace НейросетьВ2
{
    public class Net
    {
        public OutputLayer outputLayer;
        public HiddenLayer hiddenLayer;
        public List<double> allWeighs = new List<double>();
        public double ideal;

        public List<double> weights()
        {
            List<double> weights = new List<double>();
            foreach (Neuron neuron in hiddenLayer.Neurons)
                foreach (double weight in neuron.listWeightIn)
                    weights.Add(weight);
            foreach (Neuron neuron in outputLayer.Neurons)
                foreach (double weight in neuron.listWeightIn)
                    weights.Add(weight);
            return weights;
        }
        
        public Net(int inputCount, int hiddenLayerCount, int outputLayerCount, string path)
        {
            int count = hiddenLayerCount * outputLayerCount + hiddenLayerCount * inputCount;
            using (StreamReader sr = new StreamReader(path))
            {
                for (int i = 0; i < count; i++)
                {
                    allWeighs.Add(Convert.ToDouble(sr.ReadLine()));
                }
            }
            List<Neuron> hneurons = new List<Neuron>();
            List<double> hweights = new List<double>();
            for (int i = 0; i < inputCount; i++)
            {
                hweights.Add(allWeighs[i]);
            }
            for (int i = 0; i < hiddenLayerCount; i++)
            {
                hneurons.Add(new Neuron(hweights));
            }
            List<double> oweights = new List<double>(hiddenLayerCount);
            for (int i = inputCount; i < hiddenLayerCount +inputCount; i++)
            {
                oweights.Add(allWeighs[i]);
            }
            List<Neuron> oneurons = new List<Neuron>();
            for (int i = hiddenLayerCount; i < hiddenLayerCount + outputLayerCount; i++)
            {
                oneurons.Add(new Neuron(oweights));
            }
            hiddenLayer = new HiddenLayer(hneurons);
            outputLayer = new OutputLayer(oneurons);
        }
        public double[] Ot(List<double> inputs)
        {
            foreach (Neuron neuron in hiddenLayer.Neurons)
            {
                neuron.SetListIn(inputs);
            }
            List<double> Hioutputs = new List<double>();
            foreach (Neuron neuron in hiddenLayer.Neurons)
            {
                Hioutputs.Add(neuron.Output());
            }
            foreach (Neuron neuron in outputLayer.Neurons)
            {
                neuron.SetListIn(Hioutputs);
            }

            List<double> Ooutputs = new List<double>();
            foreach (Neuron neuron in outputLayer.Neurons)
            {
                Ooutputs.Add(neuron.Output());
            }
            double[] s = new double[Ooutputs.Count];
            for (int i = 0; i < Ooutputs.Count; i++)
            {
                s[i] = Ooutputs[i];
            }
            return s;
        }

        /// <summary>
        /// Обучает нейросеть на входных данных.
        /// </summary>
        /// <param name="inputs"> Массив входных данных</param>
        /// <param name="ideal"> Ожидаемое значение</param>
        /// <param name="delta"> Массив прерыдущих изменений весов</param>
        /// <param name="deltao"> Изменение весов</param>
        /// <returns>Ошибку в данном сете</returns>
        double E = 2;
        double a = 0.5;
        double error = 0;
        List<double> dOut = new List<double>();
        List<double> Ooutputs = new List<double>();
        List<double> dHid = new List<double>();

        public double Learning(List<double> inputs, double ideal, double[] delta, out double[] odelta) 
        {
            error = 0;
            dOut.Clear();
            Ooutputs.Clear();
            dHid.Clear();
            foreach (Neuron neuron in hiddenLayer.Neurons)
            {
                neuron.SetListIn(inputs);
            }


            // Выход скрытого слоя.
            List<double> Hioutputs = new List<double>();
            foreach (Neuron neuron in hiddenLayer.Neurons)
            {
                Hioutputs.Add(neuron.Output());
            }


            // Вход выходного слоя.
            foreach (Neuron neuron in outputLayer.Neurons)
            {          
                neuron.SetListIn(Hioutputs);
            }


            // Выход выходного слоя.
            foreach (Neuron neuron in outputLayer.Neurons)
            {
                Ooutputs.Add(neuron.Output());
            }
            
            // Ошибка.
            foreach (double outs in Ooutputs)
            {
                error += Math.Pow(ideal - outs, 2);
            }

            // Дельта выходного слоя.
            foreach (double output in Ooutputs)
            {
                dOut.Add((ideal - output)*((1-output)*output));
            }
            
            // Дельта скрытого слоя.
            for (int i = 0; i < Hioutputs.Count; i++)
            {
                double summ = 0;
                // Сумма предыдущих весов.
                for( int j = 0; j < outputLayer.Neurons.Count; j++)
                {
                    summ += dOut[j] * outputLayer.Neurons[j].GetListWeights(i);
                }
                dHid.Add((1 - Hioutputs[i]) * Hioutputs[i] * summ);
            }
            // Изменение весов выходого слоя.
            for (int j = 0; j < outputLayer.Neurons.Count; j++)
            {
                for (int i = 0; i < outputLayer.Neurons[j].listWeightIn.Count; i++)
                {
                    double s = E * Hioutputs[i] * dOut[j]+ a * delta[delta.Length - outputLayer.Neurons[j].listWeightIn.Count + i];
                    outputLayer.Neurons[j].listWeightIn[i] += s; 
                    delta[delta.Length - outputLayer.Neurons[j].listWeightIn.Count + i] = s;
                }
            }
            // Изменение весов скрытого слоя.
            int count = 0;
            for (int j = 0; j < hiddenLayer.Neurons.Count; j++)
            {
                for (int i = 0; i < hiddenLayer.Neurons[j].listWeightIn.Count; i++)
                {
                    double s = E * inputs[i] * dHid[j]+ a * delta[count];
                    hiddenLayer.Neurons[j].listWeightIn[i] += s;
                    delta[count] = s;
                    count++;
                }
            }
            odelta = delta;
            return error;

        }


    }
}
