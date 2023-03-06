using Observer.Издательство;
using Observer.Интерфейсы;
using Observer.Читатели;

namespace Observer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Article> articles = new List<Article>()
        {
            new Article("Кольца Власти", "Магические Кольца сделают Средиземье лучше."),
            new Article("Три Кольца", "Келебримбор завершил создание Колец Огня, Воды и Воздуха. Даже без помощи Саурона."),
            new Article("Единое Кольцо", "Саурон всех обманул и сделал кольцо для подчинения владельцев остальных колец!"),
            new Article("Восстание", "Келебримбор спрятал кольца эльфов от Саурона. Начинается восстание против зла."),
            new Article("Враг наступает", "Неприятель знает, где кольца, его армия продвигается вперед."),
            new Article("Враг разбит!", "Властелин Тьмы повержен. Большую роль в победе сыграли нуменорцы"),
            new Article("Жить становится страшно", "Лиходейские твари рядом. В Мглистых Горах множатся орки и нападают на гномов."),
            new Article("Мории не будет", "Балин предпринял всё возможное, чтобы вернуть Морию. Но ничего не получилось."),
            new Article("Множится мощь Мордора", "Мордор объявил призыв. Саруман стал предателем."),
            new Article("Рубежи падают...", "Саурон напал на Осгилиат."),
            new Article("Черные Всадники", "Черные Всадники рыскают в Хоббитландии."),
            new Article("Совет у Эльронда", "Принято решение отнести сами-знаете-что в Роковую Гору для уничтожения."),
        };
            List<Article> articles2 = new List<Article>()
        {
            new Article("Кольца Пасти", "Магические Кольца сделают Средиземье лучше."),
            new Article("Три Кольца два конца", "Келебримбор завершил создание Колец Огня, Воды и Воздуха. Даже без помощи Саурона."),
            new Article("Единое Олимпийское Кольцо", "Саурон всех обманул и сделал кольцо для подчинения владельцев остальных колец!"),
            new Article("Восстание ягнят", "Келебримбор спрятал кольца эльфов от Саурона. Начинается восстание против зла."),
            new Article("Враг наступает на...", "Неприятель знает, где кольца, его армия продвигается вперед."),
            new Article("Враг разбит толком!", "Властелин Тьмы повержен. Большую роль в победе сыграли нуменорцы"),
            new Article("Жить становится страшно весело", "Лиходейские твари рядом. В Мглистых Горах множатся орки и нападают на гномов."),
            new Article("Моря не будет", "Балин предпринял всё возможное, чтобы вернуть Морию. Но ничего не получилось."),
            new Article("Множится мощь Дамблдора", "Мордор объявил призыв. Саруман стал предателем."),
            new Article("Рубежи падают на...", "Саурон напал на Осгилиат."),
            new Article("Черные Всадники", "Черные Всадники рыскают в Хоббитландии."),
            new Article("Совет у Агента Смита", "Принято решение отнести сами-знаете-что в Роковую Гору для уничтожения."),
        };
            //Subscriber[] subscribers = new Subscriber[]
            //    {
            //        new Hobbit("Сэм"), new Orc("Маухур"), new Ent("Древобород")
            //    };
            //Subscriber[] subs2 = new Subscriber[]
            //{
            //        new Hobbit("Федор"), new Hobbit("Бульба"),
            //};
            Publisher LOTRMag = new Publisher();
            Publisher LOTR = new Publisher();
            Publisher[] pblsrs = { LOTRMag, LOTR };
            string[] command;


            //foreach (Subscriber someone in subscribers)
            //    LOTRMag.Subscribe(someone);
            //foreach (Subscriber someone in subs2)
            //    LOTR.Subscribe(someone);

            Hobbit Samwise = new Hobbit("Сэм");
            Orc Mauhur = new Orc("Маухур");
            Ent Treebeard = new Ent("Древобород");
            Hobbit Fedya = new Hobbit("Федор");
            Hobbit Bulba = new Hobbit("Бульба");

            LOTRMag.issueOfTheMagazine += Samwise.Update;
            LOTRMag.issueOfTheMagazine += Mauhur.Update;
            LOTRMag.issueOfTheMagazine += Treebeard.Update;
            LOTR.issueOfTheMagazine += Fedya.Update;
            LOTR.issueOfTheMagazine += Bulba.Update;
            LOTR.issueOfTheMagazine += Samwise.Updated;
            Console.ForegroundColor = ConsoleColor.White;
            Info();
            while (true)
            {
                do
                {
                    Console.Write(">");
                    command = Console.ReadLine().Split();
                    int numb;
                    switch (command[0])
                    {
                        case "unSub":
                            {
                                if (command.Length < 3)
                                    break;
                                //if (Convert.ToInt16(command[2]) - 1 < pblsrs.Length)
                                //{
                                //    numb = Convert.ToInt16(command[2]) - 1;
                                //    if (pblsrs[numb].Unsubscribe(pblsrs[numb].subscribers.Find(s => s.Name == command[1])))
                                //    {
                                //        MessageGood($"{command[1]} отписался от издателя {command[2]}");
                                //        break;
                                //    }
                                //    else
                                //    {
                                //        MessageBad("Данный субъект не подписан на данное издание, посмотреть все субъекты и издания - printAll");
                                //    }
                                //}
                                //else
                                //    MessageBad("Неправильный номер издателя");
                                if (Convert.ToInt16(command[2]) - 1 < pblsrs.Length)
                                {
                                   // pblsrs[Convert.ToInt16(command[2]) - 1].issueOfTheMagazine -= ;
                                }
                                else MessageBad("Неправильный номер издателя");
                                break;
                            }
                        case "printAll":
                            {
                                //Console.WriteLine($"Подписота 1 издательства:");
                                //foreach (var s in pblsrs[0].subscribers)
                                //    Console.WriteLine(s.Name);
                                //Console.WriteLine($"Подписота 2 издательства:");
                                //foreach (var s in pblsrs[1].subscribers)
                                //    Console.WriteLine(s.Name);

                                break;
                            }
                        case "sub":
                            {
                                //Console.WriteLine("Введите номер издания");
                                //numb = Convert.ToInt16(Console.ReadLine());
                                //Console.WriteLine("Введите имя читателя");
                                //string name = Console.ReadLine();
                                //Console.WriteLine("Введите расу читателя 1 - хоббит, 2 - орк, 3 - энт");
                                //switch (Convert.ToInt16(Console.ReadLine()))
                                //{
                                //    case 1: 
                                //        if(pblsrs[numb - 1].Subscribe(new Hobbit(name)))
                                //            MessageGood("Подписота добавлена");
                                //        else MessageBad("Что-то не так");
                                //        break;
                                //    case 2:
                                //        if(pblsrs[numb - 1].Subscribe(new Orc(name)))
                                //            MessageGood("Подписота добавлена");
                                //        else MessageBad("Что-то не так");
                                //        break;
                                //    case 3:
                                //        if(pblsrs[numb - 1].Subscribe(new Ent(name)))
                                //            MessageGood("Подписота добавлена");
                                //        else MessageBad("Что-то не так");
                                //        break;
                                //}
                                Console.WriteLine("Введите номер издания");
                                numb = Convert.ToInt16(Console.ReadLine());
                                Console.WriteLine("Введите имя читателя");
                                string name = Console.ReadLine();
                                Console.WriteLine("Введите расу читателя 1 - хоббит, 2 - орк, 3 - энт");
                                switch (Convert.ToInt16(Console.ReadLine()))
                                {
                                    case 1:
                                        pblsrs[numb - 1].issueOfTheMagazine += new Hobbit(name).Update;
                                        MessageGood("Подписота добавлена");
                                        break;
                                    case 2:
                                        pblsrs[numb - 1].issueOfTheMagazine += new Orc(name).Update;
                                        break;
                                    case 3:
                                        pblsrs[numb - 1].issueOfTheMagazine += new Ent(name).Update;
                                        break;
                                }
                                break;
                            }
                        case "addArt":
                            {
                                Console.WriteLine("Введите название статьи:");
                                string name = Console.ReadLine();
                                Console.WriteLine("Введите содержание статьи:");
                                string topic = Console.ReadLine();
                                Console.WriteLine("Введите номер издателя");
                                numb = Convert.ToInt16(Console.ReadLine());
                                if (numb - 1 > pblsrs.Length) { Console.WriteLine("Всего 2 издателя"); break; }
                                if (numb - 1 == 0)
                                {
                                    articles.Add(new Article(name, topic));
                                    MessageGood("Статья успешно добавлена");
                                    break;
                                }
                                if (numb - 1 == 1)
                                {
                                    articles2.Add(new Article(name, topic));
                                    MessageGood("Статья успешно добавлена");
                                    break;
                                }
                                MessageBad("Что-то не так");
                                break;
                            }
                        case "exit": Environment.Exit(0); break;
                        case "help":
                            Info(); break;
                        default:
                            Console.WriteLine("Команда не найдена, help - для помощи"); break;
                    }

                } while (command[0] != "start");
                foreach (Article article in articles2)
                    LOTRMag.AddArticle(article);
                foreach (Article article in articles)
                    LOTR.AddArticle(article);
            }
        }
        static void MessageGood(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void MessageBad(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Info()
        {
            Console.WriteLine();
            Console.WriteLine("unSub арг1 арг2 - Отписать_читателя Имя номер_издателя ");
            Console.WriteLine("sub арг1 арг2 - Подписать_читателя Имя номер_издателя ");
            Console.WriteLine("addArt - Добавить_статью");
            Console.WriteLine("exit - Закончить работы и выйти");
            Console.WriteLine("printAll - Просмотреть подписчиков изданий");
            Console.WriteLine("start - выпустить статьи издателем");
        }
    }
}