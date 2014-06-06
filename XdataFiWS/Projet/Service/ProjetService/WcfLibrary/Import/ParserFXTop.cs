using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WcfLibrary.Import
{
    /// <summary>
    /// Parser du code source HTML du site FXTop
    /// </summary>
    public class ParserFXTop : Parser
    {
        #region Attributs
        /// <summary>
        /// Chemin du fichier à parser
        /// </summary>
        string _Filepath;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur du parser FXTop
        /// </summary>
        /// <param name="filepath">chemin du fichier à parser</param>
        /// <param name="symbol">symbole correspondant aux devises à traiter de la forme DEV1/DEV2</param>
        /// <param name="langue">vaut 0 si anglais, 1 si français, à compléter si on veut plus de langues</param>
        public ParserFXTop()
        {
            //_Filepath = filepath;
            //_Symbol = symbol;
            //if (langue == 1)
            //    _ReperageLangue = "Tableau historique taux de change";
            //else
            //    _ReperageLangue = "Historical exchange rates array";
        }
        #endregion

        #region Fonction d'import
        /// <summary>
        /// Import
        /// </summary>
        /// <param name="d">base de donnée</param>
        public override void Import(Data.Data d)
        {
            //On teste le bon ordre des dates
            if (d.Fin < d.Debut)
            {
                throw new WrongDates(@"La date de fin ne peut être antérieure au début de l'acquisition");
            }

            if (d is Data.DataExchangeRate)
            {
                Data.DataExchangeRate der = (Data.DataExchangeRate) d;
                CultureInfo culture = CultureInfo.GetCultureInfo("EN");

                List<string> list;

                if (der.Freq == Data.Data.Frequency.Daily)
                    list = der.Columns;
                else
                    list = der.Symbol;

                try
                {
                    System.Net.IPHostEntry Test = System.Net.Dns.GetHostEntry("www.fxtop.com");
                }
                catch
                {
                    throw new ConnectivityException(@"Il semble que votre connection réseau soit inactive ou qu'elle ne fonctionne pas correctement, veuillez vérifier vos paramètres de connexions ou contacter votre administrateur système");
                }

                foreach (var symb in list)
                {
                    string[] monnaie = symb.Split('/');

                    //Récupération de l'url
                    //http://fxtop.com/en/historical-exchange-rates.php?A=1&C1=EUR&C2=USD&DD1=1&MM1=1&YYYY1=2012&B=1&P=&I=1&DD2=1&MM21&YYYY2=2013&btnOK=Go%21
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


                    WebClient client = new WebClient();
                    _Filepath = "FxTop.html";
                    WcfLibrary.Constantes.displayDEBUG(_Filepath, 1);

                    // Si le fichier existe, alors on le supprime
                    if (System.IO.File.Exists(@_Filepath))
                    {
                        System.IO.File.Delete(@_Filepath);
                    }

                    Uri siteUri;
                    switch (der.Type)
                    {
                        case Data.Data.TypeData.Currency:

                            siteUri = new Uri(url);

                            WcfLibrary.Constantes.displayDEBUG("start Download", 2);
                            client.DownloadFile(siteUri, _Filepath);
                            WcfLibrary.Constantes.displayDEBUG("end Download", 2);

                            WcfLibrary.Constantes.displayDEBUG("Le fichier a bien été créé", 1);
                            break;
                        //Relève une exception si c'est le mauvais type.
                        default:
                            throw new Mauvaistype(@"Un prix ne peut être négatif");
                    }

                    //chargement du fichier
                    StreamReader str = new StreamReader(_Filepath);
                    string line;

                    //Recherche de la ligne à parser
                    //CE PARSER N'EST PAS GENERIQUE, IL NE TOLERE AUCUN CHANGEMENT DE LA NORME
                    //Actuellement, une seule ligne contient toutes les données
                    while ((line = str.ReadLine()) != null)
                    {
                        if (line.Contains("Historical exchange rates array"))
                        {
                            break;
                        }
                    }

                    //On retire tout ce qui est inutile concernant les données

                    //retire le début de la ligne, </strong></td></tr> étant unique
                    string[] split = new string[] { "</strong></td></tr>" };
                    string[] ligneSplittee = line.Split(split, System.StringSplitOptions.None);

                    string[] tempSplitter = new string[1];
                    //retire la fin de la ligne
                    if (der.Freq == Data.Data.Frequency.Daily)
                        tempSplitter[0] = "</table><table border=";
                    else
                        tempSplitter[0] = "</table><br";

                    string[] temp = ligneSplittee[1].Split(tempSplitter, System.StringSplitOptions.None);

                    //La Ligne actuelle ne contient plus que ce qui concerne le tableau de données
                    //On splitte à partir des différentes balises
                    string[] splitters = new string[] { "<tr><td>", "</td><td>", "</td></tr><tr><td>", "</td></tr>", "</td><td></td></tr>", "<tr><td style=\"color:Red\">" };
                    string[] lineToParse = temp[0].Split(splitters, System.StringSplitOptions.RemoveEmptyEntries);

                    DateTime date;

                    //double av, min, max;
                    //Traitement modulo 3
                    //On traite la date, puis la valeur, et on ignore le pourcentage (qui correspond probablement à quelque chose, mais on sait pas quoi)
                    if (der.Freq == Data.Data.Frequency.Daily)
                    {
                        double val;
                        for (int i = 0; i <= lineToParse.Length; i++)
                        {
                            date = DateTime.Parse(lineToParse[i]);
                            i++;
                            val = double.Parse(lineToParse[i], culture);
                            // test la positivité du prix
                            if (val < 0)
                            {
                                throw new NegativeValueImpossible(@"Un prix ne peut être négatif");
                            }
                            i++;

                            // On insère la ligne complète
                            DataRow dr = der.Ds.Tables[0].NewRow();
                            dr["Symbol"] = der.Symbol.First();
                            dr["Date"] = date;

                            dr[symb] = val;

                            der.Ds.Tables[0].Rows.Add(dr);
                        }
                    }
                    else
                    {
                        double av, min, max;
                        ////Traitement modulo 4. Pour chaque ligne ; Version mensuelle/annuelle
                        ////On traite d'abord la date, puis l'average, puis le min, puis le max. La dernière valeur n'est volontairement pas traitée, ce qui explique un "i++" supplémentaire
                        for (int i = 0; i < lineToParse.Length; i++)
                        {
                            //Ceci est immonde, mais le format récupéré est du type MM/yyyy ou yyyy, et pas moyen de traiter ça directement
                            string s;
                            if (der.Freq == Data.Data.Frequency.Monthly)
                                s = "01/" + lineToParse[i];
                            else
                                s = "01/01/" + lineToParse[i];

                            date = DateTime.Parse(s);

                            i++;
                            av = double.Parse(lineToParse[i], culture);
                            i++;
                            min = double.Parse(lineToParse[i], culture);
                            i++;
                            max = double.Parse(lineToParse[i], culture);
                            i++;

                            // On insère la ligne complète
                            DataRow dr = der.Ds.Tables[0].NewRow();
                            dr["Symbol"] = symb;
                            dr["Date"] = date;

                            dr["Average"] = av;
                            dr["Min"] = min;
                            dr["Max"] = max;

                            der.Ds.Tables[0].Rows.Add(dr);
                        }
                    }
                    str.Close();

                    // On supprime le fichier
                    System.IO.File.Delete(@_Filepath);
                }
            }
        }
        #endregion
    }
}
