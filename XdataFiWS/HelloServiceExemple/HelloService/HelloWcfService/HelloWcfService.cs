using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ActifWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ActifWcfService : IActifWcfService
    {
        public string getActif(string name)
        {
            WebClient client = new WebClient();
            String _Filepath = "_Yahoo.csv";

            Uri siteUri = new Uri("https://fr.fiannce.yahoo.com/q?s=" + name);

            Stream data = client.OpenRead(siteUri);
            client.DownloadFile(siteUri, _Filepath);
            data.Close();

            return "OK";
        }
    }
}
