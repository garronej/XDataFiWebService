using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Globalization;
using System.Net;

namespace APIFiMag
{
    /// <summary>
    /// Classe permettant le téléchargement de données sur Yahoo Finance
    /// </summary>
    public class YQL
    {

        #region Fonctions internes à YQL

        /// <summary>
        /// Télécharge le document XML 
        /// </summary>
        /// <param name="url">Url du document à télécharger</param>
        /// <returns>Noeud results du document</returns>
        private static XElement YqlUrl(string url)
        {
            string chaineException = "Dans YqlUrl : ";
            try
            {
                //verification de l'url
                if (url == null)
                    throw new ErreurInterne("url null", true);

                //chargement du fichier XML correspondant à l'Url
                XDocument doc = XDocument.Load(url);

                //verification que le document XML n'est pas nul
                if (doc == null)
                    throw new ErreurSource("Ce symbole n'est pas accessible via YQL");

                //renvoit du noeud results du document XML
                XElement root = doc.Root;
                if (root.Elements("results").Count<XElement>() == 0)
                    throw new ErreurInterne("Pas de balise results", true);
                XElement results = root.Element("results");
                return results;

            }
            catch (ErreurSource e)
            {
                throw new ErreurSource(chaineException + "\n" + e.Message);
            }
            catch (ErreurUtilisateur e)
            {
                throw new ErreurUtilisateur(e.Message);
            }
            catch (Exception e)
            {
                throw new ErreurInterne(chaineException + "Erreur système : \n" + e.Message);
            }

        }

        /// <summary>
        /// Télécharge le document XML correspondant à une requete YQL
        /// </summary>
        /// <param name="query">requete YQL à éxécuter</param>
        /// <returns>resultat de la requete sous format d'un document XML</returns>
        private static XElement YqlQuery(string query)
        {
            String chaineException = "Dans YqlQuery : ";
            try
            {
                //verification de l'arguement
                if (query == null)
                    throw new ErreurInterne("Requete vide", true);

                //téléchargement du résultat
                String url = @"http://query.yahooapis.com/v1/public/yql?q=" + query + @"&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
                XElement results = YqlUrl(url);
                return results;
            }
            catch (ErreurUtilisateur e)
            {
                throw new ErreurUtilisateur(e.Message);
            }
            catch (ErreurSource e)
            {
                throw new ErreurSource(e.Message);
            }
            catch (Exception e)
            {
                throw new ErreurInterne(e.Message + "\n" + chaineException);
            }

        }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Renvoie le résultat d'une requête d'historique
        /// </summary>
        /// <param name="symbols">liste des symboles d'actifs</param>
        /// <param name="start">date de début</param>
        /// <param name="end">date de fin</param>
        /// <returns>XElement résultat de la requête</returns>
        public static XElement Historical(Data d, DateTime start, DateTime end)
        {
            String chaineException = "Dans Historical(sans argument Config) : ";
            try
            {
                if ((start.Date > end.Date) || (end.Date > System.DateTime.Today))
                    throw new ErreurInterne("Erreur de dates", true);
                if (d.Symbol == null)
                    throw new ErreurInterne("Requete vide", true);
                string query = "" ;

                try
                {
                    System.Net.IPHostEntry Test = System.Net.Dns.GetHostEntry("http://developer.yahoo.com/yql/console/");
                }
                catch
                {
                    throw new ConnectivityException(@"Il semble que votre connection réseau soit inactive ou qu'elle ne fonctionne pas correctement, veuillez vérifier vos paramètres de connexions ou contacter votre administrateur système");
                }


                switch (d.Type)
                {
                    case TypeData.HistoricalData:
                        query = @"http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.historicaldata where symbol = '" + d.Symbol + "' and startDate = '" + start.ToString("yyyy-MM-dd") + "' and endDate = '" + end.ToString("yyyy-MM-dd") + "'&diagnostics=true&env=store://datatables.org/alltableswithkeys";
                        break;
                    case TypeData.Currency:
                        string mastring = "(";
                        int i = 0;
                        foreach (var item in d.Columns)
                        {
                            mastring += "\"" + d.Symbol + item + "\""; 
                            if(i<d.Columns.Count)
                                mastring += ", ";
                            i++;
                        }
                        mastring += ")";
                        query = @"http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.xchange where pair in " + mastring + " ' and startDate = '" + start.ToString("yyyy-MM-dd") + "' and endDate = '" + end.ToString("yyyy-MM-dd") + "'&diagnostics=true&env=store://datatables.org/alltableswithkeys";
                        break;

                    default:
                        break;
                }
                XElement results = YqlUrl(query);
                return results;
            }
            catch (ErreurUtilisateur e)
            {
                throw new ErreurUtilisateur(e.Message);
            }
            catch (Exception e)
            {
                throw new ErreurInterne(e.Message + "\n" + chaineException);
            }

        }



        /// <summary>
        /// Effectue une requête de la table des secteurs de Yahoo Finance
        /// </summary>
        /// <returns>XElement de la table Sectors</returns>
        public static XElement Sectors()
        {
            String chaineException = "Dans la fonction Sectors : ";
            String query = @"select name from yahoo.finance.sectors";
            try
            {
                XElement results = YQL.YqlQuery(query);
                return results;

            }
            catch (ErreurUtilisateur e)
            {
                throw new ErreurUtilisateur(e.Message);
            }
            catch (Exception e)
            {
                throw new ErreurInterne(e.Message + "\n" + chaineException);
            }

        }

        /// <summary>
        /// Effectue une requête de la table Industry de Yahoo Finance
        /// </summary>
        /// <returns>XElement de la table Industry</returns>
        public static XElement Industries(String sector)
        {
            String chaineException = "Dans la fonction Industries : ";
            try
            {
                if ((sector == null))
                    throw new ErreurInterne("secteur vide", true);
                String query = @"select * from yahoo.finance.industry where id in (select industry.id from yahoo.finance.sectors where name='" + sector + "')";
                XElement results = YQL.YqlQuery(query);
                return results;

            }
            catch (ErreurUtilisateur e)
            {
                throw new ErreurUtilisateur(e.Message);
            }
            catch (Exception e)
            {
                throw new ErreurInterne(e.Message + "\n" + chaineException);
            }
        }

        /// <summary>
        /// Effectue une requête de la table Stocks de Yahoo Finance
        /// </summary>
        /// <returns>XElement de la table Stocks</returns>
        public static XElement Stocks(String nomSte)
        {
            String chaineException = "Dans la fonction Stocks : ";
            try
            {
                if (nomSte == null)
                    throw new ErreurInterne("nomSte null", true);
                String query = @"select * from yahoo.finance.stocks where symbol='" + nomSte + "'&diagnostics=true&env=store://datatables.org/alltableswithkeys";
                XElement results = YQL.YqlQuery(query);
                return results;
            }
            catch (ErreurUtilisateur e)
            {
                throw new ErreurUtilisateur(e.Message);
            }
            catch (Exception e)
            {
                throw new ErreurInterne(chaineException + e.Message);
            }

        }

        /// <summary>
        /// Effectue une requête de la table Quotes de Yahoo Finance
        /// </summary>
        /// <returns>XElement de la table Quotes</returns>
        public static XElement Quotes(String nomSte)
        {
            String chaineException = "Erreur dans la fonction Quotes : ";
            try
            {
                if (nomSte == null)
                    throw new ErreurInterne("nomSte null");
                String query = @"select * from yahoo.finance.quotes where symbol='" + nomSte + "'&diagnostics=true&env=store://datatables.org/alltableswithkeys";

                XElement results = YQL.YqlQuery(query);
                return results;
            }
            catch (ErreurUtilisateur e)
            {
                throw new ErreurUtilisateur(e.Message);
            }
            catch (Exception e)
            {
                throw new ErreurInterne(e.Message + "\n" + chaineException);
            }

        }

        /// <summary>
        /// Renvoie le prix courant de l'actif
        /// </summary>
        /// <param name="nomSte">Symbole de l'actif</param>
        /// <returns>prix courant</returns>
        public static double MonPrixTempsReel(string nomSte)
        {
            String chaineException = "Dans PrixTempsReel : ";
            double res;
            try
            {
                if (nomSte == null)
                    throw new ErreurInterne("nomSte null", true);
                String query = @"select LastTradePriceOnly from yahoo.finance.quotes where symbol='" + nomSte + "'&diagnostics=true&env=store://datatables.org/alltableswithkeys";

                XElement results = YQL.YqlQuery(query);

                if (results.Elements("quote").Count<XElement>() == 0)
                    return 0.0;
                XElement quote = results.Element("quote");
                if (quote.Elements("LastTradePriceOnly").Count<XElement>() == 0)
                {
                    return 0.0;
                }
                XElement lasttradeprice = quote.Element("LastTradePriceOnly");
                if (lasttradeprice == null)
                {
                    return 0.0;
                }
                res = Convert.ToDouble(lasttradeprice.Value, new CultureInfo("en-US"));
                return res;
            }
            catch (ErreurUtilisateur e)
            {
                throw new ErreurUtilisateur(e.Message);
            }
            catch (Exception e)
            {
                throw new ErreurInterne(e.Message + "\n" + chaineException);
            }
        }


        /// <summary>
        /// Renvoie le bid courant de l'actif
        /// </summary>
        /// <param name="nomSte">Symbole de l'actif</param>
        /// <returns>prix courant</returns>
        public static double Bid(string nomSte)
        {
            //String chaineException = "Dans Bid : ";
            double res;
            try
            {
                if (nomSte == null)
                    throw new ErreurInterne("nomSte null", true);
                String query = @"select Bid from yahoo.finance.quotes where symbol='" + nomSte + "'&diagnostics=true&env=store://datatables.org/alltableswithkeys";

                XElement results = YQL.YqlQuery(query);
                if (results.Elements("quote").Count<XElement>() == 0)
                    return 0.0;
                XElement quote = results.Element("quote");
                if (quote.Elements("Bid").Count<XElement>() == 0)
                {
                    return 0.0;
                }
                XElement myBid = quote.Element("Bid");
                if (myBid == null)
                {
                    return 0.0;
                }
                res = Convert.ToDouble(myBid.Value, new CultureInfo("en-US"));
                return res;
            }
            catch (ErreurUtilisateur e)
            {
                throw new ErreurUtilisateur(e.Message);
            }
            catch
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Renvoie l'ask courant de l'actif
        /// </summary>
        /// <param name="nomSte">Symbole de l'actif</param>
        /// <returns>prix courant</returns>
        public static double Ask(string nomSte)
        {
            double res;
            try
            {
                if (nomSte == null)
                    throw new ErreurInterne("nomSte null", true);
                String query = @"select Ask from yahoo.finance.quotes where symbol='" + nomSte + "'&diagnostics=true&env=store://datatables.org/alltableswithkeys";

                XElement results = YQL.YqlQuery(query);
                if (results.Elements("quote").Count<XElement>() == 0)
                    return 0.0;
                XElement quote = results.Element("quote");
                if (quote.Elements("Ask").Count<XElement>() == 0)
                {
                    return 0.0;
                }
                XElement myAsk = quote.Element("Ask");
                if (myAsk == null)
                {
                    return 0.0;
                }
                res = Convert.ToDouble(myAsk.Value, new CultureInfo("en-US"));
                return res;
            }
            catch (ErreurUtilisateur e)
            {
                throw new ErreurUtilisateur(e.Message);
            }
            catch
            {
                return 0.0;
            }
        }

        #endregion
    }

}