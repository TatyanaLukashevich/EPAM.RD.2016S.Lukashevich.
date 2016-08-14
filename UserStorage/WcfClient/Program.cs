using System;
using Replication;
using UserStorage;
using UserStorage.Replication;
using WcfClient.ServiceReference1;

namespace WcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User("Amy", "Levis", new DateTime(1996, 2, 17), 0);

            USContractClient client = new USContractClient();

            client.Open();

            client.Add(user);

            client.Close();
        }

    }
}
