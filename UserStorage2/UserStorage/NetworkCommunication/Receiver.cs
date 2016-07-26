using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UserStorage.NetworkCommunication
{
    [Serializable]
    public class Receiver :IDisposable
    {
        private Socket listener;
        private Socket handler;
        public IPEndPoint IpEndPoint { get; set; }

        public Receiver(IPAddress ipAddress, int port)
        {
            IpEndPoint = new IPEndPoint(ipAddress, port);
            listener = new Socket(AddressFamily.InterNetwork,
               SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(IpEndPoint);
            listener.Listen(1);
        }

        public void Dispose()
        {
            listener.Close();
            handler.Close();
        }
    }
}
