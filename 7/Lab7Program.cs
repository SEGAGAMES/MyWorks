using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 8005; // порт для приёма входящих запросов
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPHostEntry myHost = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in myHost.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    Console.WriteLine("IP-адрес сервера: " + ip.ToString());
            }

            listenSocket.Bind(ipPoint);
            do
            {
                listenSocket.Listen(10);
                Console.WriteLine("Сервер запущен. Ожидание подключений...");
                Socket hendler = listenSocket.Accept();
                Console.WriteLine("Произошло подключение!");
                processing(hendler);
                Console.WriteLine("Пользователь разорвал подключение!");
            } while (!Console.KeyAvailable || (Console.KeyAvailable && Console.ReadKey().Key != ConsoleKey.Escape));
            Console.WriteLine("End");
            Console.ReadLine();
        }

        static void processing(Socket hendler)
        {
            int bytes; // количество полученных байтов
            byte[] data; ; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();

            Calculator calculator = new Calculator();
            Random random = new Random();
            //if (hendler.Connected == false) { Console.WriteLine("unconnect"); Console.ReadKey(); }
            do
            {
                // генерирование примера
                int n = random.Next() % 5 + 2; // количество опрандов
                string example = "";

                for (int i = 0; i < n * 2 - 1; i++)
                {
                    if (i % 2 == 0) // добавление в пример операнда
                    {
                        example += (random.Next() % 20).ToString();
                    }
                    else // добавление в пример операции
                    {
                        switch (random.Next() % 3)
                        {
                            case 0: example += "+"; break;
                            case 1: example += "-"; break;
                            case 2: example += "*"; break;
                        }
                    }
                }
                string answer = calculator.Calc(example).ToString();

                Console.WriteLine(example + " = " + answer);

                // отправка примера
                data = Encoding.Unicode.GetBytes(example);
                hendler.Send(data);
                Console.WriteLine("Пример отправлен!");

                // получение ответа
                builder.Clear();
                    
                do
                {
                    try
                    {
                        bytes = hendler.Receive(data);
                    }
                    catch
                    {
                        goto mbox;
                    }
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (hendler.Available > 0);
                if (builder.ToString() == answer)
                {
                    hendler.Send(Encoding.Unicode.GetBytes("YES"));
                }
                else
                {
                    if (builder.ToString() == "end")
                        goto mbox;
                    hendler.Send(Encoding.Unicode.GetBytes("NO " + answer));
                }

            } while (builder.ToString() != "end");
            mbox:
            // закрытие сокета
            hendler.Shutdown(SocketShutdown.Both);
            hendler.Close();
        }
    }
}
