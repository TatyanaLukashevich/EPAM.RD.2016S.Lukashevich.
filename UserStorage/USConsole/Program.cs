using System;
using System.Collections.Generic;
using ConfigLayer.AppDomainConfig;
using UserStorage;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UserStorage.NetworkCommunication;
using System.Threading;

namespace UserStorageConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User("Lily", "Pod", new DateTime(1996, 2, 17), 0);
            User user2 = new User("Tracy", "Jordan", new DateTime(1996, 1, 18), 0);
            User user3 = new User("Liz", "Lemon", new DateTime(1995, 9, 9), 0);
            List<User> users = new List<User>();
            ////UserRepository repo = new UserRepository();
            SystemCreater.CreateSystem();

            SystemCreater.Master.Add(user);
            SystemCreater.Master.Add(user2);
            SystemCreater.Master.Add(user3);
            SystemCreater.Master.Repo.WriteToXML();

            var host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress receiverAddress = null;
            int receiverPort1 = 9026;
            int receiverPort2 = 9027;

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    receiverAddress = ip;

                }
            }

            var receiver = new Receiver(receiverAddress, receiverPort1);
            var receiver2 = new Receiver(receiverAddress, receiverPort2);
            var sender = new Sender();
            var task1 = StartReceiver(receiver);
            var task2 = StartReceiver(receiver2);
            var point1 = new IPEndPoint(receiverAddress, receiverPort1);
            var point2 = new IPEndPoint(receiverAddress, receiverPort2);
            sender.Connect(new List<IPEndPoint> { point1, point2 });
            Console.WriteLine("Connected!");
            Thread.Sleep(3000);
            sender.Send(new Message
            {
                User = new User
                {
                    LastName = "LastNameFromSender"
                },
                MethodType = MethodType.Add
            });
            sender.Dispose();
            receiver.Dispose();
        }

        private static Task StartReceiver(Receiver receiver)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Wait for connection");
                receiver.AcceptConnection();
                Console.WriteLine("Sender connected to receiver");
                var message = receiver.Receive();
                Console.WriteLine(message.User.LastName);
            });
        }
    }
}
