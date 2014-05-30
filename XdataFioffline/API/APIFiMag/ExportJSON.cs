using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace APIFiMag
{
    public class ExportJSON : Export
    {
        private string _FilePath;

        public ExportJSON(string filePath)
        {
            _FilePath = filePath;
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

            // nombre de colonne 
            int nbCol = d.Table.Data.Columns.Count;
            // tableau des données qu'on extrait
            List<String> donnee = new List<string>();
            for (int i = 0; i < nbCol; i++)
            {
                donnee.Add(d.Table.Data.Columns[i].ToString());
            }
            string symbole = null;
            // debut d'écriture
            sw.WriteLine("{");
            int indice = d.Table.Data.Rows.Count;
            //écriture des différentes lignes de la table Historical dans le fichier
            foreach (DataRow dr in d.Table.Data.Rows)
            {
                indice--;
                // si on a dejà recontré un symbol  
                if (symbole != null && !dr[0].ToString().Equals(symbole))
                {
                    symbole = dr[0].ToString();
                    sw.WriteLine("   },");
                    sw.WriteLine("   \"" + symbole + "\":{");
                }
                // premier passage dans la boucle
                else if (symbole == null)
                {
                    symbole = dr[0].ToString();
                    sw.WriteLine("   \"" + symbole + "\":{");
                }
                sw.WriteLine("      \"" + dr[1].ToString() + "\":{");
                for (int i = 2; i < nbCol; i++)
                {
                    if (!Convert.IsDBNull(dr[i]) && i < nbCol - 1)
                    {
                        sw.WriteLine("         \"" + donnee[i] + "\":" + "\"" + dr[i].ToString() + "\",");
                    }
                    else if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.WriteLine("         \"" + donnee[i] + "\":" + "\"" + dr[i].ToString() + "\"");
                    }
                }
                if (indice != 0)
                {
                    sw.WriteLine("      },");
                }
                else
                {
                    sw.WriteLine("      }");
                }
            }
            // on met les derniers éléments
            sw.WriteLine("   }");
            sw.WriteLine("}");
            sw.Close();
        }
    }
}
