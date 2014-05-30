using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading;

namespace AppliFiMag
{
	/// <summary>
	/// Classe de l'arbre des actifs
	/// </summary>
	public class AssetList
	{
		#region Variable globale
		/// <summary>
		/// Variable globale racine de l'arbre à créer
		/// </summary>
		//public XElement root;

		private static AssetList _Instance;

		public static AssetList Instance
		{
			get { return AssetList._Instance; }
			set { AssetList._Instance = value; }
		}

		public XDocument Doc { get; set; }

		#endregion

		#region Constructeur
		/// <summary>
		/// Constructeur de AssetList
		/// </summary>
		private AssetList()
		{
			//On ne crée l'arbre que si le fichier xml n'existe pas (sinon on charge le fichier)
			if (System.IO.File.Exists("AssetList.xml"))
			{
				Doc = XDocument.Load("AssetList.xml");
			}
			else
			{
				creeAssetList();
			}
		}

		public static void Init()
		{
			Instance = new AssetList();
		}



		#endregion

		#region Méthodes

		/// <summary>
		/// Fonction de création de l'arbre des actifs lancée dans un thread séparé
		/// </summary>
		private void creeAssetList()
		{
			// Ici root est la racine de l'arbre.
			XElement symbolTree;
			XElement root = new XElement("symbols");

			//quotes est le fils de la racine et est le sous-arbre des actifs boursiers
			XElement quotes = new XElement("quotes");
			quotes.Add(new XAttribute("name", "Quotes"));
			XElement cat, son;

			// cat est ici le fils de quotes et est l'arbre des localisations des actifs boursiers, ici on commence par ajouter l'Europe
			cat = new XElement("localisation");
			cat.Add(new XAttribute("name", "Europe"));
			quotes.Add(cat);

			//On crée les indices boursiers
			son = new XElement("quote");
			son.Add(new XAttribute("name", "CAC40"));
			son.Add(new XAttribute("symbol", "^FCHI"));
			cat.Add(son);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "Stoxx 50"));
			son.Add(new XAttribute("symbol", "^STOXX50E"));
			cat.Add(son);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "DAX (Francfort)"));
			son.Add(new XAttribute("symbol", "^GDAXI"));
			cat.Add(son);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "IBEX (Madrid)"));
			son.Add(new XAttribute("symbol", "^IBEX"));
			cat.Add(son);


			son = new XElement("quote");
			son.Add(new XAttribute("name", "Athex 20 (Athenes)"));
			son.Add(new XAttribute("symbol", "^FTASE.AT"));
			cat.Add(son);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "FTSE 100 (Londres)"));
			son.Add(new XAttribute("symbol", "^FTSE"));
			cat.Add(son);

			// Après on rajoute l'asie
			cat = new XElement("localisation");
			cat.Add(new XAttribute("name", "Asie"));
			quotes.Add(cat);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "Hang Seng (Hong Kong)"));
			son.Add(new XAttribute("symbol", "^HSI"));
			cat.Add(son);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "NSE-50 (Inde)"));
			son.Add(new XAttribute("symbol", "^NSEI"));
			cat.Add(son);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "KOSPI (Coree du Sud)"));
			son.Add(new XAttribute("symbol", "^KS11"));
			cat.Add(son);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "Nikkei 225 (Tokyo)"));
			son.Add(new XAttribute("symbol", "^N225"));
			cat.Add(son);

			// On rajoute l'Amérique
			cat = new XElement("localisation");
			cat.Add(new XAttribute("name", "Amerique"));
			quotes.Add(cat);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "Dow Jones"));
			son.Add(new XAttribute("symbol", "^DJI"));
			cat.Add(son);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "Nasdaq"));
			son.Add(new XAttribute("symbol", "^NDX"));
			cat.Add(son);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "S&P 500"));
			son.Add(new XAttribute("symbol", "^GSPC"));
			cat.Add(son);

			son = new XElement("quote");
			son.Add(new XAttribute("name", "S&P/TSX (Toronto)"));
			son.Add(new XAttribute("symbol", "^GSPTSE"));
			cat.Add(son);



			//On crée les actifs par secteur et par industrie sur le même principe que les actifs boursiers, on récupère les noms de société avec YQL
			XElement stocks = new XElement("stocks");
			stocks.Add(new XAttribute("name", "Stocks"));
			// Récupère les secteurs d'activité repertoriés par YQL
			XElement result = APIFiMag.YQL.Sectors();
			symbolTree = result;
			IEnumerable<XElement> list = symbolTree.Elements();
			foreach (XElement e in list)
			{
				// Donne le nom de l'entreprise concernée et l'ajoute à l'arbre
				result = APIFiMag.YQL.Industries(e.Attribute("name").Value);
				IEnumerable<XElement> sublist = result.Elements();
				foreach (XElement f in sublist)
				{
					e.Add(f);
				}
				stocks.Add(e);
			}



			root.Add(quotes);
			root.Add(stocks);

			//On cree le fichier .xml
			Doc = new XDocument();
			Doc.Add(root);
			Doc.Save("AssetList.xml");

		}
		#endregion
	}
}
