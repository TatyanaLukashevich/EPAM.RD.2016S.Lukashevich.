using ConfigLayer.AppDomainConfig;
using ConfigLayer.WCF;
using Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string localAddress = GetLocalIpAddress();
            var slave = new SlaveService(new UserStorage.UserRepository());
            var slave2 = new SlaveService(new UserStorage.UserRepository());
            Console.WriteLine(slave2.Name);
            //var services = SystemCreater.CreateSystem();
            //MasterService master = (MasterService)SystemCreater.Master;
            //var slaves = SystemCreater.Services.Where(s => s is SlaveService).Select(s => (SlaveService)s);
            //WcfCreator.CreateWcf(master);
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
