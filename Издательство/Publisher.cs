using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Observer.Интерфейсы;

namespace Observer.Издательство
{
    internal class Publisher
    {
        int magNumber = 0;
        int targetNumberOfArticles = 4;
        List<Article> newArticles = new List<Article>();
        Magazine currentMagazine;
        public List<Subscriber> subscribers = new List<Subscriber>();
        List<Subscriber> unsubs = new List<Subscriber>();
        List<Subscriber> addsubs = new List<Subscriber>();

        public void MainBuisnessLogic()
        {
            // Поиск материала

            // Написание статей

            if (newArticles.Count >= targetNumberOfArticles) // Выпуск журнала
            {
                magNumber++;
                currentMagazine = new Magazine(DateTime.Now, "Mag №" + magNumber, newArticles);
                NotifySubScribers();
                newArticles = new List<Article>();
            }
        }
        public void NotifySubScribers()
        {
            foreach (Subscriber s in addsubs)
                if (!subscribers.Contains(s))
                    subscribers.Add(s);
            foreach (Subscriber s in unsubs)
                if (subscribers.Contains(s))
                    subscribers.Remove(s);
            foreach (Subscriber s in subscribers)
                s.Update(this, currentMagazine);
            foreach (Subscriber s in  unsubs)
                if (subscribers.Contains(s))
                    subscribers.Remove(s);
        }

        public void Subscribe(Subscriber newSubscriber)
        {
            if (subscribers.Contains(newSubscriber) == false)
                addsubs.Add(newSubscriber);
        }
        public void Unsubscribe(Subscriber remSubscriber)
        {
            if (subscribers.Contains(remSubscriber) == true)
                unsubs.Add(remSubscriber);
        }
        public void AddArticle(Article article)
        {
            newArticles.Add(article);
            MainBuisnessLogic();
        }
    }
}
