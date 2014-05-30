using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace APIFiMag
{
    /// <summary>
    /// Implémentation de Export dans le cas de l'export en csv
    /// </summary>
    public class ExportCSV : Export
    {
        #region Attributs
        /// <summary>
        /// Chemin du fichier
        /// </summary>
        private string _FilePath;

        /// <summary>
        /// Caractère servant de séparateur (dépend de la culture)
        /// </summary>
        private string _Separator;

        /// <summary>
        /// Format des dates
        /// </summary>
        private string _DateFormat;
        #endregion

        #region Constructeur

        /// <summary>
        /// Constructeur de ExportCSV
        /// </summary>
        /// <param name="filePath">Chemin du fichier</param>
        /// <param name="culture">culture (généralement fr ou en)</param>
		public ExportCSV(string filePath, CultureInfo culture)
		{
			_FilePath = filePath;
			if (culture.TwoLetterISOLanguageName =="fr")
			{
				_Separator = ";";
				_DateFormat = "dd/MM/yyyy";
			}
			else
			{
				_Separator = ",";
				_DateFormat = "MM/dd/yyyy";
			}
		}

        /// <summary>
        /// Constructeur de ExportCSV
        /// </summary>
        /// <param name="filePath"></param>
		public ExportCSV(string filePath)
			: this(filePath, CultureInfo.CurrentUICulture)
		{
		}
        #endregion

        #region Export
        /// <summary>
        /// Exportation des données historiques dans un fichier CSV
        /// </summary>
        /// <param name="d">Base de donnée à exporter</param>
		public void Export(Data d)
		{
			//throw new NotImplementedException();
			//string chaineException = "Dans exportCSV : ";
			if (System.IO.File.Exists(@_FilePath))
			{
				//le fichier existe : on le supprime
				System.IO.File.Delete(@_FilePath);
			}

            //création d'un StreamWriter pour pouvoir écrire dans le fichier
            StreamWriter sw = new StreamWriter(_FilePath);
            string col = "Actif";
            col += _Separator;
            col += "Date";

            //écriture sur la première ligne du fichier du nom des différentes colonnes
            int nbCol = d.Columns.Count + 1;
            foreach (var item in d.Columns)
            {
                col += _Separator;
                col += item;
            }
            sw.WriteLine(col);

            var actifs = (from x in d.Table.Data
                          group x by x.Symbol into y
                          select y);

            foreach (var symbol in actifs)
            {

                //écriture des différentes lignes de la table Historical dans le fichier
                var rows = (from x in symbol
                            group x by x.Date into y
                            select y);

                foreach (var date in rows)
                {
                    sw.Write(symbol.Key);
                    sw.Write(_Separator);
                    sw.Write(date.Key.ToString(_DateFormat));
                    foreach (var item in d.Columns)
                    {
                        sw.Write(_Separator);
                        var val = (from x in date
                                   where x.Column == item
                                   select x.Value).First();
                        sw.Write(val);
                    }
                    sw.Write(sw.NewLine);
                }
            }
            sw.Close();

        }
        #endregion
    }
}
