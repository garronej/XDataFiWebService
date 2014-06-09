using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single)]
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Service1 : IService1
    {
        public void DoWork()
        {
            string name = "client.data";
            // Si le fichier existe, alors on le supprime
            if ( ! System.IO.File.Exists(@name))
            {
                System.IO.File.Create(@name);
            }
            StreamReader str = new StreamReader(name);
            string line = str.ReadLine();

            //int nb = int.Parse(line);
            Console.WriteLine(line);
            System.Threading.Thread.Sleep(5000);
        }
    }
}
