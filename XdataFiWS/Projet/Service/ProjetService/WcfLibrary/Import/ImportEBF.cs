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
    /// Implémentation de Import dans le cas des taux d'intérêts
    /// </summary>
    public class ImportEBF : ParserCSV
    {
        #region Méthodes d'import
        /// <summary>
        /// Méthode de connexion
        /// </summary>
        public ImportEBF()
            : base("", CultureInfo.GetCultureInfo("EN"), true, "", true)
        {
            try
            {
                System.Net.IPHostEntry Test = System.Net.Dns.GetHostEntry("www.euribor-ebf.eu");
            }
            catch
            {
                throw new ConnectivityException(@"Il semble que votre connection réseau soit inactive ou qu'elle ne fonctionne pas correctement, veuillez vérifier vos paramètres de connexions ou contacter votre administrateur système");
            }
        }

        /// <summary>
        /// Implémentation de Import, taux d'intérêts
        /// </summary>
        /// <param name="d">Base de donnée</param>
        public override void Import(Data.Data d)
        {
            for (int i = d.Debut.Year; i <= d.Fin.Year; i++)
            {
                _CurrentSymbol = d.Symbol.First();

                _Filepath = "EBF_" + _CurrentSymbol + "_" + i +".csv";
                WcfLibrary.Constantes.displayDEBUG(_Filepath, 1);

                // Si le fichier existe, alors on le supprime
                if (System.IO.File.Exists(@_Filepath))
                {
                    //le fichier existe : on le supprime
                    System.IO.File.Delete(@_Filepath);
                }

                Uri siteUri;
                switch (d.Type)
                {
                    case Data.Data.TypeData.InterestRate:
                        // exemple d'url voulu : http://www.euribor-ebf.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2013.csv

                        siteUri = new Uri("http://www.euribor-ebf.eu/assets/modules/rateisblue/processed_files/hist_" + _CurrentSymbol + "_" + i + ".csv");

                        WcfLibrary.Constantes.displayDEBUG("start Download", 2);

                        WebClient client = new WebClient();
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
