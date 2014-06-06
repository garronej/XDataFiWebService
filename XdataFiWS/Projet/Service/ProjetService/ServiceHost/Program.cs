using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ProjetServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceHost hostActif = new ServiceHost(typeof(Service.ActifService));
            ServiceHost hostIRate = new ServiceHost(typeof(Service.Services));

            //hostActif.Open();
            hostIRate.Open();

            Console.WriteLine("Host started @" + DateTime.Now.ToString());
            Console.ReadLine();

            //hostActif.Close();
            hostIRate.Close();
        }
    }
}
