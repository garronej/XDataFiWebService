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
    /// Implémentation de Import, dans le cas de la récupéation d'actifs depuis la plate forme de Google
    /// </summary>
    public class ImportGoogle : ParserCSV
    {
        #region Méthodes d'import

        /// <summary>
        /// Méthode de connexion
        /// </summary>
        public ImportGoogle()
            : base("", CultureInfo.GetCultureInfo("EN"), true)
        {
            try
            {
                System.Net.IPHostEntry Test = System.Net.Dns.GetHostEntry("www.google.fr");
            }
            catch
            {
                throw new ConnectivityException(@"Il semble que votre connection réseau soit inactive ou qu'elle ne fonctionne pas correctement, veuillez vérifier vos paramètres de connexions ou contacter votre administrateur système");
            }
        }

        /// <summary>
        /// Implémentation de Import, actifs
        /// </summary>
        /// <param name="d"></param>
        public override void Import(Data d)
        {
            if (d is Datas.DataActif)
            {
                var dtf = (Datas.DataActif)d;
                foreach (var symbol in dtf.Symbol)
                {
                    _CurrentSymbol = symbol;
                    WebClient client = new WebClient();
                    _Filepath = "Google_" + symbol + ".csv";
                    if (System.IO.File.Exists(@_Filepath))
                    {
                        //le fichier existe : on le supprime
                        System.IO.File.Delete(@_Filepath);
                    }
                    Uri siteUri;
                    switch (dtf.Type)
                    {
                        case TypeData.HistoricalData:
                            // exemple d'url voulu : https://www.google.com/finance/historical?q=GOOG&startdate=May+8+2011&enddate=Jun+8%2C+2013&num=30&output=csv
                            siteUri = new Uri("http://www.google.com/finance/historical?q=" + symbol + "&startdate=" + dtf.Debut.ToString("MMM+d+yyyy") + "&enddate=" + dtf.Fin.ToString("MMM+d+yyyy") + "&num=30&output=csv");
                            Stream data = client.OpenRead(siteUri);
                            client.DownloadFile(siteUri, _Filepath);
                            data.Close();
                            break;
                        default:
                            throw new Mauvaistype(@"Mauvais Type utilisé");
                    }
                    base.Import(dtf);
                    System.IO.File.Delete(@_Filepath);
                }
            }
        }
        #endregion
    }

}
