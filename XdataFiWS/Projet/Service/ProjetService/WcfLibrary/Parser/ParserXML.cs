using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WcfLibrary.Parser
{
    /// <summary>
    /// Parser pour les ficheirs XML
    /// </summary>
    class ParserXML : Parser
    {
        #region Attributs
        /// <summary> Nom du fichier à parser  </summary>
        protected string _FilepathSchema;

        /// <summary> Tableau des symboles à traiter </summary>
        private string[] _TableauSymboles;

        /// <summary> Nom du noeud contenant la date </summary>
        private string _NomNoeudDate;

        /// <summary> Nom du noeud contenant le symbole </summary>
        private string _NomNoeudSymb;

        /// <summary> Nom du noeud contenant la valeur </summary>
        private string _NomNoeudVal;

        /// <summary> Nom du noeud Data </summary>
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
        public ParserXML()
        {
        }

        public ParserXML(string filepath, string filepathSchema)
        {
            _Filepath = filepath;
            _FilepathSchema = filepathSchema;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Parse le fichier désiré
        /// Insère les données dans d
        /// </summary>
        /// <param name="d">base de donnée</param>
        public override void ParseFile(Data.Data d)
        {
            WcfLibrary.Constantes.displayDEBUG("start parseXML", 2);

            try
            {
                d.Ds.ReadXmlSchema(_FilepathSchema);
                WcfLibrary.Constantes.displayDEBUG("ReadXmlSchema Done", 3);

                d.Ds.ReadXml(_Filepath, XmlReadMode.ReadSchema);
                WcfLibrary.Constantes.displayDEBUG("ReadXml Done", 3);
            }
            catch(Exception e)
            {
                WcfLibrary.Constantes.displayDEBUG(e.Message, 3);
            }

            WcfLibrary.Constantes.displayDEBUG("end parseXML", 2);

            /*
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

                DataRow dr = d.Ds.Tables[0].NewRow();
                dr["Symbol"] = d.Symbol.First();
                dr["Date"] = DateTime.Parse(x[_NomNoeudDate].InnerText);

                // x[_NomNoeudSymb].InnerText
                foreach (string colonne in d.Columns)
                {
                    dr[colonne] = Double.Parse(x[_NomNoeudVal].InnerText);
                }

                d.Ds.Tables[0].Rows.Add(dr);

            }*/
        }
        #endregion

    }
}
