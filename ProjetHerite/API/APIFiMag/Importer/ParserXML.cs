using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace APIFiMag
{   
    /// <summary>
    /// Parser pour les ficheirs XML
    /// </summary>
	class ParserXML : Parser
	{
		#region Attributs
		/// <summary>
		/// Chemin du fichier à importer
		/// </summary>
		private string _Filepath;

		/// <summary>
		/// Tableau des symboles à traiter
		/// </summary>
		private string[] _TableauSymboles;

		/// <summary>
		/// Nom du noeud contenant le date
		/// </summary>
		private string _NomNoeudDate;

		/// <summary>
		/// Nom du noeud contenant le symbole
		/// </summary>
		private string _NomNoeudSymb;

		/// <summary>
		/// Nom du noeud contenant la valeur
		/// </summary>
		private string _NomNoeudVal;

		/// <summary>
		/// Nom du noeud Data
		/// </summary>
		private string _NomNoeudPere;
		#endregion

        #region Constructeur
        /// <summary>
		/// Constructeur
		/// Accepte les fichiers XML du type suivant :
		/// Une liste de noeuds data contenant chacun 3 noeuds correspondant aux valeurs
		/// </summary>
		/// <param name="filepath">Chemin du fichier</param>
		/// <param name="nnd">Nom du noeud date</param>
		/// <param name="nns">Nom du noeud symbole</param>
		/// <param name="nnp">Nom du noeud père contentant les 3 noeuds</param>
		/// <param name="nnv">Nom du noeud valeur</param>
		/// <param name="tab">Tableau correspondant aux symboles à traiter</param>
		public ParserXML(string filepath, string nnd, string nns, string nnp, string nnv, string[] tab)
		{
			_Filepath = filepath;
			_NomNoeudDate = nnd;
			_NomNoeudPere = nnp;
			_NomNoeudSymb = nns;
			_NomNoeudVal = nnv;
			_TableauSymboles = new string[tab.Length];
			Array.Copy(tab, _TableauSymboles, tab.Length);
		}
        #endregion

        #region Méthode d'import
        /// <summary>
		/// Fonction d'import depuis du XML
		/// </summary>
		/// <param name="d">base de donnée</param>
        public override void Import(Data d)
		{
            //On teste le bon ordre des dates
            if (d.Fin < d.Debut)
            {
                throw new WrongDates(@"La date de fin ne peut être antérieure au début de l'acquisition");
            }

			XmlDocument doc = new XmlDocument();
			doc.Load(_Filepath);
			//On récupère la liste des noeuds data
			XmlNodeList noeudsData = doc.GetElementsByTagName(_NomNoeudPere);
			foreach (XmlNode x in noeudsData)
			{
				//Si le Symbole n'est pas dans le tableau, on ignore
				if (!(_TableauSymboles.Contains(x[_NomNoeudSymb].InnerText)))
					continue;

                // test la positivité du prix
                if (Double.Parse(x[_NomNoeudVal].InnerText) < 0)
                {
                    throw new NegativeValueImpossible(@"Un prix ne peut être négatif");
                }

				d.Table.Data.AddDataRow(DateTime.Parse(x[_NomNoeudDate].InnerText),d.Symbol.First(), x[_NomNoeudSymb].InnerText, Double.Parse(x[_NomNoeudVal].InnerText));
			}
        }
        #endregion

    }
}
