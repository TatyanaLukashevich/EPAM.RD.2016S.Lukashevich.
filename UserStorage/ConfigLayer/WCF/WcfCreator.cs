using Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConfigLayer.WCF
{
    public class WcfCreator
    {
        public static ServiceHost CreateWcf(Service service)
        {
           
            ServiceHost host = new ServiceHost(service);
            return host;
        }


    }
}
