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
            Subscriber[] subscribers = new Subscriber[]
                {
                    new Hobbit("Сэм"), new Orc("Маухур"), new Ent("Древобород")
                };
            Subscriber[] subs2 = new Subscriber[]
            {
                    new Hobbit("Федор"), new Hobbit("Бульба"),
            };
            Publisher LOTRMag = new Publisher();
            Publisher LOTR = new Publisher();
            Publisher[] pblsrs = { LOTRMag, LOTR };
            string[] command;
            foreach (Subscriber someone in subscribers)
                LOTRMag.Subscribe(someone);
            
            foreach (Subscriber someone in subs2)
                LOTR.Subscribe(someone);
            Console.ForegroundColor = ConsoleColor.White;
            do

            {
                Console.WriteLine("unSub арг1 арг2 - Отписать_читателя Имя номер_издателя ");
                Console.WriteLine("sub арг1 арг2 - Подписать_читателя Имя номер_издателя ");
                Console.WriteLine("addArt арг1 арг2 арг3 - Добавить_статью Назание_статьи Текст_статьи Номер_издания");
                Console.WriteLine("exit - Закончить работы и выйти");
                Console.Write(">");

                command = Console.ReadLine().Split();
                switch (command[0])
                {
                    case "exit": Environment.Exit(0); break;
                    case "unSub":
                        if (Convert.ToInt16(command[2]) - 1 < pblsrs.Length)
                        {
                            foreach (var s in pblsrs[Convert.ToInt16(command[2]) - 1].subscribers)
                            {
                                if (s.Name == Convert.ToString(command[1]))
                                {
                                    pblsrs[Convert.ToInt16(command[2]) - 1].Unsubscribe(s);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"{command[1]} отписался от издателя");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                }
                            }
                        }
                        else { Console.WriteLine("Неправильный номер издателя"); }
                        break;
                    case "sub":
                        if (Convert.ToInt16(command[2]) - 1 < pblsrs.Length)
                        {
                            foreach (var s in pblsrs[Convert.ToInt16(command[2]) - 1].subscribers)
                            {
                                if (s.Name == Convert.ToString(command[1]))
                                {
                                    pblsrs[Convert.ToInt16(command[2]) - 1].Subscribe(s);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"{command[1]} подписался на издателя");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                }
                            }
                        }
                        else { Console.WriteLine("Неправильный номер издателя"); }
                        break;
                    case "addArt":
                        int numb = Convert.ToInt16(command[3]) - 1;
                        if (numb == 0)
                        {
                            articles.Add(new Article(command[1], command[2]));
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Статья успешно добавлена");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (numb == 1)
                        {
                            articles2.Add(new Article(command[1], command[2]));
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Статья успешно добавлена");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                }
                
            } while (command[0] != "exit");
            foreach (Article article in articles2)
                LOTRMag.AddArticle(article);
            foreach (Article article in articles)
                LOTR.AddArticle(article);
        }
    }
}