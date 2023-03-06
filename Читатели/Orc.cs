using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Observer.Издательство;
using Observer.Интерфейсы;

namespace Observer.Читатели
{
    internal class Orc //: Subscriber
    {
        public string Name { get; }
        public Orc(string name)
        {
            Name = name;
        }
        public void Update(Publisher sender, object context)
        {
            Magazine magazine = (Magazine)context;
            foreach (Article article in magazine.Articles)
                Read(article, sender);
        }

        private void Read(Article article, Publisher sender)
        {
            //if (article.Title.Contains("Эльф") || article.Title.Contains("эльф") || article.Text.Contains("Эльф") || article.Text.Contains("эльф") )
            //    sender.Unsubscribe(this);
            //else
                Console.WriteLine($"{Name} читает статью \"{article.Title}\"");
        }
    }
}
