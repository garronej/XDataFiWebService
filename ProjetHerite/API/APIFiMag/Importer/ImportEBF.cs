using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Net;

namespace APIFiMag.Importer
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
			: base("", CultureInfo.GetCultureInfo("EN"), true,"",true)
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
        public override void Import(Data d)
		{
			for (int i = d.Debut.Year; i <= d.Fin.Year; i++)
			{
                _CurrentSymbol = d.Symbol.First();
				WebClient client = new WebClient();
                _Filepath = "EBF_" + _CurrentSymbol + ".csv";
				if (System.IO.File.Exists(@_Filepath))
				{
					//le fichier existe : on le supprime
					System.IO.File.Delete(@_Filepath);
				}
				Uri siteUri;
				switch (d.Type)
				{
					case TypeData.InterestRate:
						// exemple d'url voulu : http://www.euribor-ebf.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2013.csv
                        siteUri = new Uri("http://www.euribor-ebf.eu/assets/modules/rateisblue/processed_files/hist_" + _CurrentSymbol + "_" + i + ".csv");
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
