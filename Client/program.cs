using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // адрес и порт сервера, к которому будем подключаться
            int port = 8005; // порт сервера
            string address = "127.0.0.1"; // адрес сервера
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            StringBuilder builder = new StringBuilder();
            string message;
            byte[] data = new byte[256];
            int bytes;
            //try
            //{
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                do
                {
                    // получение ответа
                    do
                    {
                        bytes = socket.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (socket.Available > 0);
                    Console.WriteLine("Ответа сервера: " + builder.ToString());
                    builder.Clear(); 
                    // отправка сообщения
                    Console.Write("Введите сообщение: ");
                    message = Console.ReadLine();
                    data = Encoding.Unicode.GetBytes(message);
                    socket.Send(data);
                    data = new byte[256];
                    // получение ответа
                    do
                    {
                        bytes = socket.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    } while (socket.Available > 0);
                    if (builder.Length == 0)
                        goto mbox;
                    if (builder[0] != 'N')
                        Console.WriteLine("Ответа сервера: " + builder.ToString());
                    else
                    {
                        string ans = "0";
                        while (ans != builder.ToString().Split()[1])
                        {
                            Console.WriteLine("NO");
                            ans = Console.ReadLine();

                            if (ans == "ans")
                            {
                                Console.WriteLine(builder.ToString().Split()[1]);
                                break;
                            }
                            Console.WriteLine(ans);
                        }
                        if (ans == builder.ToString().Split()[1])
                            Console.WriteLine("Yes!");
                    }
                    builder.Clear();

                } while (message != "end");
            mbox:
                Console.WriteLine("end.");

                // закрытие сокета
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
           // }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }
    }
}
