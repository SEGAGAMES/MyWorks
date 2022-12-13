using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class WorkWithClient
    {
        static int count = 0;
        Socket socket;
        int id;
        string clientInfo;
        public WorkWithClient(Socket socket)
        {
            this.socket = socket;
            id = count;
            count++;
            clientInfo = socket.RemoteEndPoint.ToString();
        }

        string answer;
        StringBuilder builder = new StringBuilder();
        byte[] data;
        int dataLength;
        public void Run()
        {
            Console.WriteLine($"Client №{id}. Информация: Установлено соединение с \"{clientInfo}\"");
            try
            {
                do
                {
                    // Получение команды
                    data = new byte[256];
                    builder.Clear();
                    do
                    {
                        dataLength = socket.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, dataLength));
                    } while (socket.Available > 0);
                    Console.WriteLine($"Client №{id}. Команда: {builder.ToString()}");
                    // Обработка команды для генерации ответа
                    answer = DateTime.Now.ToString();
                    // Отправка ответа клиенту
                    data = Encoding.Unicode.GetBytes(answer);
                    socket.Send(data);
                } while (builder.ToString() != "exit");
                Console.WriteLine($"Client №{id}. Информация: Клиент отключился");
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine($"Client №{id}. Ошибка: {e.Message}");
            }
        }
    }
}
