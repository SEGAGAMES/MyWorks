using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
     static internal class ClientBroadCast
    {
        static string? command;
        static byte[] data = new byte[256];
        static int bytes;
        static StringBuilder builder = new StringBuilder();
        static public void BroadCast(Socket socket)
        {
            while (true)
            {
                // Получение ответа.
                while (socket.Available > 0)
                {
                    bytes = socket.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } //while (socket.Available > 0);
                if (builder.Length > 0)
                    Console.WriteLine("Ответа сервера: " + builder.ToString());
                builder.Clear();

                // Отправка сообщения.
                Console.Write("Введите сообщение: ");
                do
                    command = Console.ReadLine();
                while (command == null);
                if (command == "exit") break;
                data = Encoding.Unicode.GetBytes(command);
                socket.Send(data);
                data = new byte[256];

                // Получение ответа.
                do
                {
                    bytes = socket.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                if (builder.Length != 0)
                    Console.WriteLine("Ответа сервера: " + builder.ToString());
                builder.Clear();

            }

            // Закрытие сокета.
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
