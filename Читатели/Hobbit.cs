﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Observer.Издательство;
using Observer.Интерфейсы;

namespace Observer.Читатели
{
    internal class Hobbit : Subscriber
    {
        public string Name { get; }

        public Hobbit(string name)
        {
            Name = name;
        }
        public void Update(Publisher sender, object context)
        {
            Magazine magazine = (Magazine)context;
            for (int i = magazine.Articles.Length - 1; i > -1; i--)
            {
                Read(magazine.Articles[i]);
            }
        }

        private void Read(Article article)
        {
            Console.WriteLine($"{Name} читает статью \"{article.Title}\"");
        }
    }
}
