using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WcfLibrary.Data
{
    /// <summary>
    /// Implémentation de Data dans le cas de la création de la BD à partir d'un fichier XML de configuration
    /// </summary>
    public class DataXML : Data
    {

        /// <summary>
        /// Construction à partir d'un fichier XML de configuration
        /// </summary>
        /// <param name="filepath">Chemin du fichier</param>
        public DataXML()
            : base()
        {
            
            /*
            XmlNode noeudSymbol = doc["RecupData"]["ListeIndex"];
            Symbol = new List<string>();
            //Traitement des index ou des composantes, suivant le cas
            if (!(noeudSymbol.HasChildNodes))
            {
                noeudSymbol = doc["RecupData"]["ListeComposantes"];
            }
            XmlNodeList noeudsindex = noeudSymbol.ChildNodes;
            //Ajout aux symboles
            foreach (XmlNode x in noeudsindex)
                Symbol.Add(x.InnerText);


            Columns = new List<string>();

            //Traitement des colonnes
            XmlNode noeudColonnes = doc["RecupData"]["Champs"];
            XmlNodeList noeudsparam = noeudColonnes.ChildNodes;
            foreach (XmlNode x in noeudsparam)
                Columns.Add(x.InnerText);

            Debut = DateTime.Parse(doc["RecupData"]["DateDebut"].InnerText);
            Fin = DateTime.Parse(doc["RecupData"]["DateFin"].InnerText);
            */
        }
    }
}
