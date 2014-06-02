using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ActifService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "ActifService" à la fois dans le code et le fichier de configuration.
    public class ActifService : IActifService
    {
        public string getActif(string name)
        {
            WebClient client = new WebClient();
            String _Filepath = "_Yahoo.csv";

            Uri siteUri = new Uri("https://fr.finance.yahoo.com/q?s=" + name);

            Stream data = client.OpenRead(siteUri);
            StreamReader str = new StreamReader(data);
            String code = str.ReadToEnd();

            code = code.Substring(code.IndexOf("class=\"time_rtq_ticker\">") + 24);
            code = code.Substring(code.IndexOf(">") +1);

            code = code.Substring(0, code.IndexOf("<"));
            //client.DownloadFile(siteUri, _Filepath);
            //data.Close();

            return code;
        }

        
    }
}
