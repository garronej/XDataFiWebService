using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace APIFiMag
{
    class ParserXML :Parser
    {
        /// <summary>
        /// Chemin du fichier à importer
        /// </summary>
        private string _Filepath;

        private string[] _TableauSymboles;

        private string _NomNoeudPere;

        public override void Import(Data d)
        {
            XmlDocument doc = new XmlDocument();

            doc.Load(_Filepath);
            XmlNodeList noeudsData = doc.GetElementsByTagName(_NomNoeudPere);
            foreach ( var x in noeudsData)
            {

            }    


            throw new NotImplementedException();
        }
    }
}
