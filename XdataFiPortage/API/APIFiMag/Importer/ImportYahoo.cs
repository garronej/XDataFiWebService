using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Globalization;

namespace APIFiMag.Importer
{   
    /// <summary>
    /// Implémentation de Import dans le cas de la récupération d'actifs depuis la plateforme de Yahoo
    /// </summary>
    public class ImportYahoo : ParserCSV
    {
        #region Méthodes d'import
        /// <summary>
        /// Méthode de connexion
        /// </summary>
        public ImportYahoo()
            : base("", CultureInfo.GetCultureInfo("EN"), true)
        {
            try
            {
                System.Net.IPHostEntry Test = System.Net.Dns.GetHostEntry("fr.finance.yahoo.com");
            }
            catch
            {
                throw new ConnectivityException(@"Il semble que votre connection réseau soit inactive ou qu'elle ne fonctionne pas correctement, veuillez vérifier vos paramètres de connexions ou contacter votre administrateur système");
            }
        }

        /// <summary>
        /// Implémentation de import
        /// </summary>
        /// <param name="d">Base de donnée</param>
        public override void Import(Data d)
        {
            foreach (var symbol in d.Symbol)
            {
                _CurrentSymbol = symbol;
                WebClient client = new WebClient();
                _Filepath = "Yahoo_" + symbol + "_" + d.Debut.ToString("dd-MM-yy") + "_" + d.Fin.ToString("dd-MM-yy") + ".csv";
                if (System.IO.File.Exists(@_Filepath))
                {
                    //le fichier existe : on le supprime
                    System.IO.File.Delete(@_Filepath);
                }
                Uri siteUri;
                switch (d.Type)
                {
                    case TypeData.HistoricalData:
                        // exemple d'url voulu : http://ichart.finance.yahoo.com/table.csv?s=GOOG&d=5&e=8&f=2013&g=d&a=5&b=8&c=2011&ignore=.csv

                        int moiDebt = d.Debut.Month - 1;
                        int moiFin = d.Fin.Month - 1;

                        siteUri = new Uri("http://ichart.finance.yahoo.com/table.csv?s=" + symbol + "&d=" + moiFin + "&e=" + d.Fin.Day + "&f=" + d.Fin.Year + "&g=d" + "&a=" + moiDebt + "&b=" + d.Debut.Day + "&c=" + d.Debut.Year + "&ignore=.csv");
                        client.DownloadFile(siteUri, _Filepath);
                        break;
                    default:
                        throw new Mauvaistype(@"Mauvais Type utilisé");
                }
                base.Import(d);
                System.IO.File.Delete(@_Filepath);
            }
        }
        #endregion
    }
}
