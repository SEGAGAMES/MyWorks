using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Client
{
    internal class ClientMain
    {
        static void Main(string[] args)
        {
            string? ip;
            int port;
            mbox:
            // Aдрес и порт сервера, к которому будем подключаться.
            do
            {
                Console.WriteLine("Введите IP адрес сервера");
                ip = Console.ReadLine();
            } while (ip == "");
            do
                Console.WriteLine("Введите порт сервера");
            while(!int.TryParse(Console.ReadLine(), out port));
            ClientConnect connect = new ClientConnect(port, ip);

            // Подключаемся к удаленному хосту.
            try
            {
                connect.Socket.Connect(connect.IpPoint);
            }
            catch { Console.WriteLine("Произошла ошибка подключения, повторите попытку"); goto mbox; }

            Console.WriteLine("Подключение к серверу произошло успешно!");
            ClientBroadCast.BroadCast(connect.Socket);
        }
    }
}