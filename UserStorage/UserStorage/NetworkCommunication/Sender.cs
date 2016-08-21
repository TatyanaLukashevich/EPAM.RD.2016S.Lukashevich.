using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using NLog;

namespace UserStorage.NetworkCommunication
{
    /// <summary>
    /// Sender class
    /// </summary>
    [Serializable]
    public class Sender : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private List<Socket> sockets = new List<Socket>();

        /// <summary>
        /// Connect to listeners
        /// </summary>
        /// <param name="ipEndPoints"></param>
        public void Connect(IEnumerable<IPEndPoint> ipEndPoints)
        {
            foreach (var ipEndPoint in ipEndPoints)
            {
                var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEndPoint);
                sockets.Add(socket);
            }
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="message"></param>
        public void Send(Message message)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            foreach (var socket in sockets)
            {
                using (var networkStream = new NetworkStream(socket))
                {
                   formatter.Serialize(networkStream, message);
                }
            }
           
            Logger.Info("Message send!");
        }

        /// <summary>
        /// Close sockets
        /// </summary>
        public void Dispose()
        {
            foreach (var socket in sockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}
