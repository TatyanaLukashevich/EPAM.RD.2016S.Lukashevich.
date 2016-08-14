using Replication;
using System;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;

namespace ConfigLayer.WCF
{
    public class WcfCreator
    {
        public static ServiceHost CreateWcf(Service service)
        {
            string localAddress = GetLocalIpAddress();
            Uri serviceUri = new Uri($"http://{localAddress}:8080/Service/" + service.Name);
            ServiceHost host = new ServiceHost(service, serviceUri);
            host.Open();

            #region Output dispatchers listening
            foreach (Uri uri in host.BaseAddresses)
            {
                Console.WriteLine("\t{0}", uri.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Number of dispatchers listening : {0}", host.ChannelDispatchers.Count);
            foreach (System.ServiceModel.Dispatcher.ChannelDispatcher dispatcher in host.ChannelDispatchers)
            {
                Console.WriteLine("\t{0}, {1}", dispatcher.Listener.Uri.ToString(), dispatcher.BindingName);
            }
            Console.WriteLine();
            #endregion

            return host;
        }

        private static string GetLocalIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

    }
}
