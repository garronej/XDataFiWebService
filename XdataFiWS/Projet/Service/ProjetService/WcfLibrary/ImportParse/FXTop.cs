using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WcfLibrary.ImportParse
{
    /// <summary>
    /// Récupération du code source HTML du site FXTop
    /// Traitement de celui-ci via le ParserFXTop
    /// </summary>
    public class FXTop : ImportParse
    {
        #region Constructeur
        /// <summary> Constructeur du parser FXTop </summary>
        public FXTop()
        {
            _Parser = new Parser.ParserFXTop();

            // Test la connectivité réseau
            try
            {
                System.Net.IPHostEntry Test = System.Net.Dns.GetHostEntry("www.fxtop.com");
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
        /// <param name="d">Base de donnée, doit être de type ExchangeRate</param>
        public override void ImportAndParse(Data.Data d)
        {
            // On vérifie que les données soient de type ExchangeRate
            switch (d.Type)
            {
                case Data.Data.TypeData.ExchangeRate:

                    //On teste le bon ordre des dates
                    if (d.Fin < d.Debut)
                    {
                        throw new WrongDates(@"La date de fin ne peut être antérieure au début de l'acquisition");
                    }

                    // Cast, pour obtenir la fréquence
                    Data.DataExchangeRate der = (Data.DataExchangeRate) d;
                    CultureInfo culture = CultureInfo.GetCultureInfo("EN");

                    List<string> list;

                    if (der.Freq == Data.Data.Frequency.Daily)
                        list = der.Columns;
                    else
                        list = der.Symbol;

                    // Pour chaque symbol, on récupère le fichier et on le parse
                    foreach (var symb in list)
                    {
                        string[] monnaie = symb.Split('/');

                        //Récupération de l'url
                        //http://fxtop.com/en/historical-exchange-rates.php?A=1&C1=EUR&C2=USD&DD1=1&MM1=1&YYYY1=2012&B=1&P=&I=1&DD2=1&MM21&YYYY2=2013&btnOK=Go%21
                        //http://fxtop.com/en/historical-exchange-rates.php?A=1&C1=EUR&C2=USD&MA=1&DD1=01&MM1=05&YYYY1=2014&B=1&P=&I=1&DD2=01&MM2=06&YYYY2=2014&btnOK=Go%21
                        //http://fxtop.com/en/historical-exchange-rates.php?A=1&C1=ADF&C2=ALL&MA=1&DD1=1&MM1=4&YYYY1=2014&B=1&P=&I=1&DD2=1&MM2=5&YYYY2=2014&btnOK=Go%21
                        string url;

                        if (der.Freq == Data.Data.Frequency.Daily)
                            url = "http://fxtop.com/en/historical-exchange-rates.php?A=1&C1=" + monnaie[0] + "&C2=" + monnaie[1] + "&DD1=" + der.Debut.ToString("dd") + "&MM1=" + der.Debut.ToString("MM") + "&YYYY1=" + der.Debut.ToString("yyyy") + "&B=1&P=&I=1&DD2=" + der.Fin.ToString("dd") + "&MM2=" + der.Fin.ToString("MM") + "&YYYY2=" + der.Fin.ToString("yyyy") + "&btnOK=Go%21";
                        else
                        {
                            string choixFreq;

                            if (der.Freq == Data.Data.Frequency.Monthly)
                                choixFreq = "&MA=1";
                            else
                                choixFreq = "&YA=1";

                            url = "http://fxtop.com/en/historical-exchange-rates.php?A=1&C1=" + monnaie[0] + "&C2=" + monnaie[1] + choixFreq + "&DD1=" + der.Debut.ToString("dd") + "&MM1=" + der.Debut.ToString("MM") + "&YYYY1=" + der.Debut.ToString("yyyy") + "&B=1&P=&I=1&DD2=" + der.Fin.ToString("dd") + "&MM2=" + der.Fin.ToString("MM") + "&YYYY2=" + der.Fin.ToString("yyyy") + "&btnOK=Go%21";
                        }

                        _Filepath = "FxTop_" + monnaie[0] + "_" + monnaie[1] + "_" + der.Freq.ToString() 
                                  + "_" + der.Debut.ToString("dd-MM-yy") + "_" + der.Fin.ToString("dd-MM-yy") + ".html";
                        Uri siteUri = new Uri(url);

                        // Télécharge le fichier
                        ImportFile(siteUri);

                        // On indique au parser le symbol courant et le nom du fichier, puis on parse le fichier obtenu
                        _Parser.set(_Filepath, symb);
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
