using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using NLog;

namespace UserStorage.NetworkCommunication
{
    [Serializable]
    public class Receiver : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private Socket listener;

        private Socket handler;

        public IPEndPoint IpEndPoint { get; set; }

        public Receiver(IPAddress ipAddress, int port)
        {
            IpEndPoint = new IPEndPoint(ipAddress, port);
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(IpEndPoint);
            listener.Listen(1);
        }

        public Task AcceptConnection()
        {
            return Task.Run(() =>
            {
                Logger.Info("Wait Connection");
                handler = listener.Accept();
                Logger.Info("Connection accepted");
            });
        }

        public Message Receive()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Message message;

            using (var networkStream = new NetworkStream(handler, false))
            {
                message = (Message)formatter.Deserialize(networkStream);
            }

            Logger.Info("Message was received");
            return message;
        }

        public void Dispose()
        {
            listener.Close();
            handler.Close();
        }
    }
}
