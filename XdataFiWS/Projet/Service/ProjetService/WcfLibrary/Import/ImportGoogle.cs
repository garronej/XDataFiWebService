using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WcfLibrary.Import
{
    /// <summary>
    /// Implémentation de Import, dans le cas de la récupéation d'actifs depuis la plate forme de Google
    /// </summary>
    public class ImportGoogle : ParserCSV
    {
        #region Constructeur
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
        #endregion

        #region Méthodes
        /// <summary>
        /// Implémentation de Import, actifs
        /// </summary>
        /// <param name="d"></param>
        public override void Import(Data.Data d)
        {
            foreach (string symbol in d.Symbol)
            {
                _CurrentSymbol = symbol;
                WebClient client = new WebClient();

                _Filepath = "Google_" + symbol + ".csv";
                WcfLibrary.Constantes.displayDEBUG(_Filepath, 1);

                // Si le fichier existe, alors on le supprime
                if (System.IO.File.Exists(@_Filepath))
                {
                    System.IO.File.Delete(@_Filepath);
                }

                Uri siteUri;
                switch (d.Type)
                {
                    case Data.Data.TypeData.HistoricalData:
                        // exemple d'url voulu : https://www.google.com/finance/historical?q=GOOG&startdate=May+8+2011&enddate=Jun+8%2C+2013&num=30&output=csv
                        //https://www.google.com/finance/historical?q=CA.PA&startdate=juin+1+2014&enddate=juin+3+2014&num=30&output=csv

                        siteUri = new Uri("http://www.google.com/finance/historical?q=" + symbol + "&startdate=" + d.Debut.ToString("MMM+d+yyyy") + "&enddate=" + d.Fin.ToString("MMM+d+yyyy") + "&num=30&output=csv");
                        
                        WcfLibrary.Constantes.displayDEBUG("start Download", 2);
                        client.DownloadFile(siteUri, _Filepath);
                        WcfLibrary.Constantes.displayDEBUG("end Download", 2);

                        WcfLibrary.Constantes.displayDEBUG("Le fichier a bien été créé", 1);
                        break;
                    default:
                        throw new Mauvaistype(@"Mauvais Type utilisé");
                }

                // On traite le fichier csv obtenu
                base.Import(d);

                // On supprime le fichier
                System.IO.File.Delete(@_Filepath);
            }
        }
        #endregion
    }
}
