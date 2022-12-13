using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    /// <summary>
    /// Подключение к серверу.
    /// </summary>
    internal class ClientConnect
    {
        int port;
        string address;
        
        public ClientConnect(int port, string address)
        {
            Port = port;
            Address = address;
            IPAddress? check;
            if (!IPAddress.TryParse(address, out check)) { Console.WriteLine("Некорректный айпи адрес"); return; }
            IpPoint = new IPEndPoint(check, Port);
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        /// <summary>
        /// Полный адрес сервера для подключения.
        /// </summary>
        public IPEndPoint IpPoint { get; }
        /// <summary>
        /// Сокет для передачи данных.
        /// </summary>
        public Socket Socket { get; }
        /// <summary>
        /// Порт сервера.
        /// </summary>
        public int Port { get => port; set => port = value; }
        /// <summary>
        /// IP-адрес сервера.
        /// </summary>
        public string Address { get => address; set => address = value; }
    }
}
