using System;
using System.Globalization;
using System.Linq;
using System.Net;

namespace WcfLibrary.ImportParse
{
    /// <summary>
    /// Récupération des taux d'intérêts depuis "www.euribor-ebf.eu"
    /// (Récupération d'un fichier csv)
    /// Traitement via ParserCSV
    /// </summary>
    public class EBF : ImportParse
    {
        #region Attributs
        /// <summary> année courante à récupérer </summary>
        private int _CurrentYear;
        #endregion

        #region Constructeur
        /// <summary>
        /// Méthode de connexion
        /// </summary>
        public EBF()
        {
            _Parser = new Parser.ParserCSV("", CultureInfo.GetCultureInfo("EN"), true, "", true);

            // Test la connectivité réseau
            try
            {
                System.Net.IPHostEntry Test = System.Net.Dns.GetHostEntry("www.euribor-ebf.eu");
            }
            catch
            {
                throw new ConnectivityException(@"Il semble que votre connection réseau soit inactive ou qu'elle ne fonctionne pas correctement, veuillez vérifier vos paramètres de connexions ou contacter votre administrateur système");
            }
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Télécharge le fichier désiré
        /// et la parse, en remplissant les données
        /// </summary>
        /// <param name="d">Base de donnée, doit être de type InterestRate</param>
        public override void ImportAndParse(Data.Data d)
        {
            // On vérifie que les données soient de type InterestRate
            switch (d.Type)
            {
                case Data.Data.TypeData.InterestRate:

                    //On teste le bon ordre des dates
                    if (d.Fin < d.Debut)
                    {
                        throw new WrongDates(@"La date de fin ne peut être antérieure au début de l'acquisition");
                    }

                    // Pour chaque année, on récupère le fichier et on le parse
                    for (int i = d.Debut.Year; i <= d.Fin.Year; i++)
                    {
                        _CurrentYear = i;
                        string symbol = d.Symbol.First();
                        _Filepath = "EBF_" + symbol + "_" + _CurrentYear + ".csv";

                        Uri siteUri;
                        // exemple d'url voulu : http://www.euribor-ebf.eu/assets/modules/rateisblue/processed_files/hist_EURIBOR_2013.csv

                        siteUri = new Uri("http://www.euribor-ebf.eu/assets/modules/rateisblue/processed_files/hist_" + symbol + "_" + _CurrentYear + ".csv");

                        // Télécharge le fichier
                        ImportFile(siteUri);

                        // On indique au parser le symbol courant et le nom du fichier, puis on parse le fichier obtenu
                        _Parser.set(_Filepath, symbol);
                        _Parser.ParseFile(d);

                        // On supprime le fichier
                        System.IO.File.Delete(@_Filepath);
                    }
                    break;
                default:
                    throw new Mauvaistype(@"Mauvais Type utilisé");
            }
        }
        #endregion
    }
}
