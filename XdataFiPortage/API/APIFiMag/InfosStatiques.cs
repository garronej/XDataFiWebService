using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Globalization;
using System.Threading;

namespace APIFiMag
{
    /// <summary>
    /// Classe des infos générales des sociétés
    /// </summary>
    public class InfosStatiques
    {
        #region Attributs
        /// <summary>
        /// type des infos récupérées
        /// </summary>
        public enum infosDispo { Name, Start, End, Secteur, Industrie, NbEmployes, PlaceBoursiere, Ebitda, Capitalisation, Isin } ;
        /// <summary>
        /// Infos récupérées sous forme de string
        /// </summary>
        public string[] ValeursCourantes { get; set; }
        /// <summary>
        /// Document qu 'on charge pour conversion isin-ticker
        /// </summary>
        private static XDocument doc = new XDocument();
        #endregion



        #region Constructeur
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="ste">symbole de la société dont on veut les infos</param>
        public InfosStatiques(String ste)
        {
            string chaineException = "Dans le constructeur InfoStatiques : ";
            try
            {
                ValeursCourantes = new string[10];


                //Récupération de toutes les données sur Yahoo Finance
                XElement xetemp;
                XElement stocks = YQL.Stocks(ste);
                XElement quote = YQL.Quotes(ste);

                if (stocks.Elements("stock").Count<XElement>() == 0)
                    throw new ErreurInterne("Pas de balise stock", true);
                xetemp = stocks.Element("stock");

                // intitialisation du doc de conversion isin - tick
                setDoc();
                string name = ste;
                ConvertTickerToName(ref name);
                if (!name.Equals(ste))
                {
                    ValeursCourantes[(int)infosDispo.Name] = name;
                }

                if (stocks.Element("stock").Elements("FullTimeEmployees").Count<XElement>() == 0)
                    throw new ErreurInterne("Pas de balise FullTimeEmployees", true);
                xetemp = stocks.Element("stock").Element("FullTimeEmployees");
                if (xetemp == null)
                    throw new ErreurInterne("Pas de valeur pour FullTimeEmployees", true);
                ValeursCourantes[(int)infosDispo.NbEmployes] = xetemp.Value;


                if (stocks.Element("stock").Elements("Industry").Count<XElement>() == 0)
                    throw new ErreurInterne("Pas de balise Industry", true);
                xetemp = stocks.Element("stock").Element("Industry");
                if (xetemp == null)
                    throw new ErreurInterne("Pas de valeur pour Industry", true);
                ValeursCourantes[(int)infosDispo.Industrie] = xetemp.Value;

                if (stocks.Element("stock").Elements("Sector").Count<XElement>() == 0)
                    throw new ErreurInterne("Pas de balise Sector", true);
                xetemp = stocks.Element("stock").Element("Sector");
                if (xetemp == null)
                    throw new ErreurInterne("Pas de valeur pour Sector", true);
                ValeursCourantes[(int)infosDispo.Secteur] = xetemp.Value;

                if (stocks.Element("stock").Elements("start").Count<XElement>() == 0)
                    throw new ErreurInterne("Pas de balise start", true);
                xetemp = stocks.Element("stock").Element("start");
                if (xetemp == null)
                    throw new ErreurInterne("Pas de valeur pour start", true);
                ValeursCourantes[(int)infosDispo.Start] = xetemp.Value;

                if (stocks.Element("stock").Elements("end").Count<XElement>() == 0)
                    throw new ErreurInterne("Pas de balise end", true);
                xetemp = stocks.Element("stock").Element("end");
                if (xetemp == null)
                    throw new ErreurInterne("Pas de valeur pour end", true);
                ValeursCourantes[(int)infosDispo.End] = xetemp.Value;

                if (quote.Elements("quote").Count<XElement>() == 0)
                    throw new ErreurInterne("Pas de balise quote", true);
                xetemp = quote.Element("quote");

                if (quote.Element("quote").Elements("MarketCapitalization").Count<XElement>() == 0)
                    throw new ErreurInterne("Pas de balise MarketCapitalization", true);
                xetemp = quote.Element("quote").Element("MarketCapitalization");
                if (xetemp == null)
                    throw new ErreurInterne("Pas de valeur pour MarketCapitalization", true);
                ValeursCourantes[(int)infosDispo.Capitalisation] = xetemp.Value;

                if (quote.Element("quote").Elements("EBITDA").Count<XElement>() == 0)
                    throw new ErreurInterne("Pas de balise EBITDA", true);
                xetemp = quote.Element("quote").Element("EBITDA");
                if (xetemp == null)
                    throw new ErreurInterne("Pas de valeur pour EBITDA", true);
                ValeursCourantes[(int)infosDispo.Ebitda] = xetemp.Value;

                if (quote.Element("quote").Elements("StockExchange").Count<XElement>() == 0)
                    throw new ErreurInterne("Pas de balise StockExchange", true);
                xetemp = quote.Element("quote").Element("StockExchange");
                if (xetemp == null)
                    throw new ErreurInterne("Pas de valeur pour StockExchange", true);
                ValeursCourantes[(int)infosDispo.PlaceBoursiere] = xetemp.Value;
                string isin = ste;
                ConvertTickerToIsin(ref isin);
                if (!isin.Equals(ste))
                {
                    ValeursCourantes[(int)infosDispo.Isin] = isin;
                }

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
        #endregion

        #region Méthodes statiques de traitement
        ///<summary>
        /// Méthode static permettant de convertir un code en ISIN en ticker
        /// </summary>
        /// <param name="isin">Code ISIN à convertir en ticker</param>
        public static void ConvertIsinToTicker(ref string isin)
        {
            // Recherche dans le fichier Xml contenant 
            // la table de correspondance ISIN/Tick
            XElement table = doc.Root;
            IEnumerable<XElement> liste = table.Elements();

            foreach (XElement e in liste)
            {
                if ((e.Element("ISIN")).Value.Equals(isin))
                {
                    isin = e.Element("ticker").Value;
                }
            }
        }
        ///<summary>
        /// Méthode static permettant de convertir un ticker en nom de compagnie
        /// </summary>
        /// <param name="isin">le code ISIN à convertir en ticker, mofifé si conversion possible</param>   
        public static void ConvertTickerToName(ref string ticker)
        {
            // Recherche dans le fichier Xml contenant 
            // la table de correspondance ISIN/Tick
            XElement table = doc.Root;
            IEnumerable<XElement> liste = table.Elements();
            // On vérifie qu'il n'y a pas de suffixe, sinon on enlève le suffixe
            if (ticker.Length > 3 && ticker.Substring(ticker.Length - 3, 1) == ".")
            {
                ticker = ticker.Substring(0, ticker.Length - 3);
            }

            foreach (XElement e in liste)
            {
                if ((e.Element("ticker")).Value.Equals(ticker))
                {
                    ticker = e.Element("nom").Value;
                }
            }
        }

        ///<summary>
        /// Méthode permettant de convertir un ticker en code ISIN
        /// </summary>
        /// <param name="ticker">le ticker à convertir en code ISIN, modifié à la sortie</param>         
        public static void ConvertTickerToIsin(ref string ticker)
        {
            // Recherche dans le fichier Xml contenant 
            // la table de correspondance ISIN/Tick
            XElement table = doc.Root;
            IEnumerable<XElement> liste = table.Elements();
            // On vérifie qu'il n'y a pas de suffixe, sinon on enlève le suffixe
            if (ticker.Length > 3 && ticker.Substring(ticker.Length - 3, 1) == ".")
            {
                ticker = ticker.Substring(0, ticker.Length - 3);
            }

            foreach (XElement e in liste)
            {
                if ((e.Element("ticker")).Value.Equals(ticker))
                {
                    ticker = e.Element("ISIN").Value;
                }
            }
        }

        /// <summary>
        /// Methode static de mise à jour de doc
        /// </summary>
        public static void setDoc()
        {
            if (System.IO.File.Exists("IsinToTick.xml"))
            {
                doc = XDocument.Load("IsinToTick.xml");
            }
        }
        #endregion

        #region Accès aux données
        /// <summary>
        /// Fonction d'accès aux infos
        /// </summary>
        /// <param name="info">valeur du type indiquant l'info souhaitée</param>
        /// <returns>info souhaitée</returns>
        public string AccesInfos(infosDispo info)
        {
            string chaineException = "Dans AccesInfos : ";
            try
            {

                return ValeursCourantes[(int)info];

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
        #endregion

    }
}

