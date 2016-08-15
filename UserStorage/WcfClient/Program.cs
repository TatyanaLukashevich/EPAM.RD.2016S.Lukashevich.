using NLog;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using WcfClient.ServiceReference;

namespace WcfClient
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            try
            {
                User user = new User
                {
                    IDk__BackingField = 1,
                    Namek__BackingField = "Bruce",
                    LastNamek__BackingField = "Wayne",
                    UserGenderk__BackingField = Gender.Male,
                    DateOfBirthk__BackingField = new DateTime(1996, 2, 17),
                    VisaRecordsk__BackingField = new List<VisaRecord>().ToArray()
                };
                USContractClient client = new USContractClient();
                client.Open();
                user.IDk__BackingField = client.Add(user);
                //client.Delete(user);
                client.WriteToXML();
                int[] ids = client.FindByTag(new[] { new GenderMaleCriteria() });

            }

            catch (FaultException excp)
            {
                Console.WriteLine(excp.Message);
                Logger.Trace(excp.Message);
            }
        }

    }
}
