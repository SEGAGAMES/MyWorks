using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Observer.Интерфейсы;
using Observer.Издательство;

namespace Observer.Читатели
{
    internal class People : Subscriber
    {
        public string Name { get; }
        public People(string name)
        {
            Name = name;
        }

        public void Update(Publisher sender, object context)
        {
            Magazine magazine = (Magazine)context;
            foreach (Article article in magazine.Articles)
                Read(article);
        }

        private void Read(Article article)
        {
            Console.WriteLine($"{Name} читает статью \"{article.Title}\"");
        }
    }
}
