using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace APIFiMag
{   
    /// <summary>
    /// Implémentation de Export dans le cas de l'export en XML
    /// </summary>
    public class ExportXML : Export
    {
        #region Attributs

        /// <summary>
        /// Chemin du fichier
        /// </summary>
        private string _FilePath;
        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur de ExportXML
        /// </summary>
        /// <param name="filePath">Chemin du fichier</param>
		public ExportXML(string filePath)
		{
			_FilePath = filePath;
		}
        #endregion

        #region Export
        /// <summary>
        /// Méthode d'exportation de données (taux interbancaires, cours d'actifs) vers un fichier XML
        /// </summary>
        /// <param name="d">Base de données à exporter</param>
		public void Export(Data d)
		{
			if (System.IO.File.Exists(@_FilePath))
			{
				//le fichier existe : on le supprime
				System.IO.File.Delete(@_FilePath);
			}
			StreamWriter w = new StreamWriter(_FilePath);
			d.Table.Data.WriteXml(w);
			w.Flush();
			w.Close();
        }
        #endregion
    }
}