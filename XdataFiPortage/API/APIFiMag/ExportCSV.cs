using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace APIFiMag
{
    public enum Langue
    {
        FR,
        EN
    }

    public class ExportCSV : Export
    {
        private string _FilePath;
        private string _Separator;
        private string _DateFormat;

        public ExportCSV(string filePath, Langue l)
        {
            _FilePath = filePath;
            switch (l)
            {
                case Langue.FR:
                    _Separator = ";";
                    _DateFormat = "dd/MM/yyyy";
                    break;
                case Langue.EN:
                    _Separator = ",";
                    _DateFormat = "MM/dd/yyyy";
                    break;
                default:
                    _Separator = ",";
                    _DateFormat = "MM/dd/yyyy";
                    break;
            }
        }

        public ExportCSV(string filePath)
            : this(filePath, Langue.EN)
        {
        }

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
            string col = "Date";

            //écriture sur la première ligne du fichier du nom des différentes colonnes
            int nbCol = d.Symbols.Count + 1;
            foreach (var item in d.Symbols)
            {
                col += _Separator;
                col += item;
            }
            sw.WriteLine(col);

            //écriture des différentes lignes de la table Historical dans le fichier
            for (DateTime t = d.Debut; t <= d.Fin; t = t.Add(d.Frequence))
            {
                sw.Write(t.ToString(_DateFormat));
                foreach (var item in d.Symbols)
                {
                    sw.Write(_Separator);
                    var val = (from x in d.Table.Data
                               where x.Date == t && x.Symbol == item
                               select x.Value).First();
                    sw.Write(val);
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
