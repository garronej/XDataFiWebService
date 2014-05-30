using System;
using System.Collections.Generic;
using System.IO;
using System.Data;

namespace APIFiMag
{   
    /// <summary>
    /// Implémentation de Export dans le cas de l'export en JSON
    /// </summary>
    public class ExportJSON : Export
    {
        #region Attributs
        /// <summary>
        /// Chemin du fichier
        /// </summary>
        private string _FilePath;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de ExportJSON
        /// </summary>
        /// <param name="filePath"></param>
        public ExportJSON(string filePath)
        {
            _FilePath = filePath;
        }
        #endregion

        #region Export
        /// <summary>
        /// Exportation des données historiques dans un fichier JSON
        /// </summary>
        /// <param name="d">Base de donnée à exporter</param>
        public void Export(Data d)
        {

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
        #endregion
    }
}
