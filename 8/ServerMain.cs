using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace Сервер_удалённого_доступа
{
    internal class ServerMain
    {
        static int port = 2022;
        static IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, port);
        static Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static void Main()
        {

            listenSocket.Bind(ipPoint);
            listenSocket.Listen(10);
            do
            {
                Socket handler = listenSocket.Accept();
                Console.WriteLine($"Входящее подключение от {handler.RemoteEndPoint}");

                // Создание объекта для работы с соединением в отдельном потоке.
                WorkWithClient work = new WorkWithClient(handler);
                ThreadStart start = new ThreadStart(work.Run);
                Thread thr1 = new Thread(start);
                thr1.Start();

            } while (true);

        } 
    }
}
