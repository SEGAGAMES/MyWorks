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
        public delegate void Update(Publisher publisher, Magazine magazine);
        public event Update issueOfTheMagazine;
        //public List<Subscriber> subscribers = new List<Subscriber>();
        //List<Subscriber> unsubs = new List<Subscriber>();

        public void MainBuisnessLogic()
        {
            // Поиск материала

            // Написание статей

            if (newArticles.Count >= targetNumberOfArticles) // Выпуск журнала
            {
                Console.WriteLine();
                Console.WriteLine("Издатель выпустил новый журнал!");
                magNumber++;
                currentMagazine = new Magazine(DateTime.Now, "Mag №" + magNumber, newArticles);
                if (issueOfTheMagazine != null)
                    issueOfTheMagazine(this, currentMagazine);
                //NotifySubScribers();
                newArticles = new List<Article>();
            }
        }
        //public void NotifySubScribers()
        //{
        //    foreach (Subscriber s in unsubs)
        //        if (subscribers.Contains(s))
        //            subscribers.Remove(s);
        //    foreach (Subscriber s in subscribers)
        //        s.Update(this, currentMagazine);
        //    foreach (Subscriber s in  unsubs)
        //        if (subscribers.Contains(s))
        //            subscribers.Remove(s);
        //}

        //public bool Subscribe(Subscriber newSubscriber)
        //{
        //    if (subscribers.Contains(newSubscriber) == false)
        //    {
        //        subscribers.Add(newSubscriber);
        //        return true;
        //    }
        //    else return false;
        //}
        //public bool Unsubscribe(Subscriber remSubscriber)
        //{
        //    if (subscribers.Contains(remSubscriber) == true)
        //    {
        //        unsubs.Add(remSubscriber);
        //        return true;
        //    }
        //    else return false;
        //}
        public void AddArticle(Article article)
        {
            newArticles.Add(article);
            MainBuisnessLogic();
        }
    }
}
