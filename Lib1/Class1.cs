﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Lib1
{
    [Serializable]
    public class Planet
    {
        string name;
        double D;
        int number;

        public Planet() { }
        public Planet(int number, string name, double D)
        {
            Name = name;
            Diam = D;
            Number = number;
        }
        public static void Masses(Planet a, Planet b)
        {
            double div = Math.Round(a.D / b.D,2);
            Console.WriteLine($"Отношение диаметров планеты {a.Name} и планеты {b.Name} равно {div}");
        }
        [XmlElement("Имя")]
        public string Name { get => name; set => name = value; }
        public double Diam { get => D; set => D = value; }
        public int Number { get => number; set => number = value; }
        public void Info()
        {
            Console.WriteLine($"\nНазвание планеты {this.Name}, диаметр планеты {this.Diam} тыс.км., номер планеты от Солнца {this.Number}");
        }
    }
}
