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
        public override void Import(Data d)
        {
            //On teste le bon ordre des dates
            if (d.Fin < d.Debut)
            {
                throw new WrongDates(@"La date de fin ne peut être antérieure au début de l'acquisition");
            }
            
            if (d is Datas.DataFXTop)
            {
                var dtf = (Datas.DataFXTop)d;
            CultureInfo culture = CultureInfo.GetCultureInfo("EN");
            List<string> list;

            if (dtf.Freq == Frequency.Daily)
                list = dtf.Columns;
            else
                list = dtf.Symbol;

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

                if (dtf.Freq == Frequency.Daily)
                    url = "http://fxtop.com/en/historical-exchange-rates.php?A=1&C1=" + monnaie[0] + "&C2=" + monnaie[1] + "&DD1=" + dtf.Debut.ToString("dd") + "&MM1=" + dtf.Debut.ToString("MM") + "&YYYY1=" + dtf.Debut.ToString("yyyy") + "&B=1&P=&I=1&DD2=" + dtf.Fin.ToString("dd") + "&MM2=" + dtf.Fin.ToString("MM") + "&YYYY2=" + dtf.Fin.ToString("yyyy") + "&btnOK=Go%21";
                else
                {
                    string choixFreq;

                    if (dtf.Freq == Frequency.Monthly)
                        choixFreq = "&MA=1";
                    else
                        choixFreq = "&YA=1";

                    url = "http://fxtop.com/en/historical-exchange-rates.php?A=1&C1=" + monnaie[0] + "&C2=" + monnaie[1] + choixFreq + "&DD1=" + dtf.Debut.ToString("dd") + "&MM1=" + dtf.Debut.ToString("MM") + "&YYYY1=" + dtf.Debut.ToString("yyyy") + "&B=1&P=&I=1&DD2=" + dtf.Fin.ToString("dd") + "&MM2=" + dtf.Fin.ToString("MM") + "&YYYY2=" + dtf.Fin.ToString("yyyy") + "&btnOK=Go%21";
                }


                

                WebClient client = new WebClient();
                _Filepath = "FxTop.html";

                if (System.IO.File.Exists(@_Filepath))
                {
                    //le fichier existe : on le supprime
                    System.IO.File.Delete(@_Filepath);
                }

                Uri siteUri;

                switch (dtf.Type)
                {
                    case TypeData.Currency:
                        siteUri = new Uri(url);
                        client.DownloadFile(siteUri, _Filepath);
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
                if (dtf.Freq == Frequency.Daily)
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
                if (dtf.Freq == Frequency.Daily)
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

                        DataSet.DataRow row = dtf.Table.Data.NewDataRow();
                        row.Date = date;
                        row.Value = val;
                        row.Symbol = dtf.Symbol.First();
                        row.Column = symb;
                        dtf.Table.Data.AddDataRow(row);
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
                        if (dtf.Freq == Frequency.Monthly)
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

                        DataSet.DataRow row1 = dtf.Table.Data.NewDataRow();
                        DataSet.DataRow row2 = dtf.Table.Data.NewDataRow();
                        DataSet.DataRow row3 = dtf.Table.Data.NewDataRow();

                        row1.Date = date;
                        row2.Date = date;
                        row3.Date = date;

                        row1.Column = "AVERAGE";
                        row2.Column = "MIN";
                        row3.Column = "MAX";

                        row1.Symbol = symb;
                        row2.Symbol = symb;
                        row3.Symbol = symb;

                        row1.Value = av;
                        row2.Value = min;
                        row3.Value = max;

                        dtf.Table.Data.AddDataRow(row1);
                        dtf.Table.Data.AddDataRow(row2);
                        dtf.Table.Data.AddDataRow(row3);
                    }
                }
                str.Close();
                System.IO.File.Delete(@_Filepath);
            }
            }
        }
        #endregion
    }
}
