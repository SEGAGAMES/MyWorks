using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Сервер_удалённого_доступа
{
    internal class Program
    {
        static int port = 2022;
        
            static IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, port);
            static Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            static void Main()
            {
                try
                {
                    listenSocket.Bind(ipPoint);
                    listenSocket.Listen(10);
                    do
                    {
                        Socket handler = listenSocket.Accept();
                        Console.WriteLine($"Входящее подключение от {handler.RemoteEndPoint}");
                        // Создание объекта для работы с соединением в отдельном потоке
                    } while (true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } 
    }
}
