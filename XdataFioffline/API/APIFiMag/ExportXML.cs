using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace APIFiMag
{
    public class ExportXML : Export
    {
        private string _FilePath;

        /// <summary>
        /// Constructeur de ExportXML
        /// </summary>
        /// <param name="filePath"></param>
		public ExportXML(string filePath)
		{
			_FilePath = filePath;
		}

        /// <summary>
        /// Méthode d'exportation de données (taux interbancaires, cours d'actifs) vers un fichier XML
        /// </summary>
        /// <param name="d"></param>
		public void Export(Data d)
		{
			if (System.IO.File.Exists(@_FilePath))
			{
				//le fichier existe : on le supprime
				System.IO.File.Delete(@_FilePath);
			}

			d.Table.Data.WriteXml(@_FilePath);
		}
	}
}