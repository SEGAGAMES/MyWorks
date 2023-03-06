using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Observer.Издательство;
using Observer.Интерфейсы;

namespace Observer.Читатели
{
    internal class Ent : Subscriber
    {
        public string Name { get; }

        public Ent(string name)
        {
            Name = name;
        }
        public void Update(Publisher sender, object context)
        {
            Magazine magazine = (Magazine)context;
            Random rnd = new Random();
            int[] readed = new int[magazine.Articles.Length];
            for (int i = 0; i < magazine.Articles.Length; i++)
            {
                int numb;
                do
                {
                    numb = rnd.Next(1, magazine.Articles.Length + 1);
                }
                while (readed.Contains(numb));
                Read(magazine.Articles[numb - 1]);
                readed[i] = numb;
            }
        }

        private void Read(Article article)
        {
            Console.WriteLine($"{Name} читает статью \"{article.Title}\"");
        }
    }
}
