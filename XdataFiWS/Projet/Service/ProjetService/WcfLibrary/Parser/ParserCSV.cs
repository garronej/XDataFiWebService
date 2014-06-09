using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WcfLibrary.Parser
{
    /// <summary>
    /// Parser pour les fichiers CSV
    /// </summary>
    public class ParserCSV : Parser
    {
        #region Attributs

        /// <summary> Correspond à la culture du fichier importé. le format varie en fonction de la langue utilisée </summary>
        private CultureInfo _Culture;
        /// <summary> Séparateur voulu, suivant la norme du fichier Csv (lié à la culture)</summary>
        private char _Separateur;

        /// <summary> Vaut true si le nom des colonnes est présent </summary>
        private bool _Nom;

        /// <summary> Vaut true si la matrice des données est transposée </summary>
        private bool _Transpose;
        /// <summary> Nom de la colonne de date </summary>
        private string _NomDateColonne;

        #endregion

        #region Constructeurs

        /// <summary>
        /// Constructeur avancé
        /// </summary>
        /// <param name="filepath">Chemin du fichier</param>
        /// <param name="culture">Culture, sert pour les séparateurs</param>
        /// <param name="colNamed">Indique si le nom des colonnes est indiqué dans le fichier</param>
        /// <param name="nomDateColonne">Nom de la colonne contenant la date</param>
        /// <param name="transpose">Indique si la matrice des données a besoin d'être transposée pour être traitée</param>
        public ParserCSV(string filepath, CultureInfo culture, bool colNamed, string nomDateColonne, bool transpose)
        {
            _Filepath = filepath;
            _Culture = culture;
            if (_Culture == CultureInfo.GetCultureInfo("FR"))
                _Separateur = ';';
            else
                _Separateur = ',';
            _Nom = colNamed;
            _NomDateColonne = nomDateColonne;
            _Transpose = transpose;
        }

        /// <summary>
        /// Constructeur 
        /// </summary>
        /// <param name="filepath">Chemin d'accès du fichier</param>
        /// <param name="culture">Culture, sert pour les séparateurs</param>
        /// <param name="colNamed">Indique si le nom des colonnes est indiqué dans le fichier</param>
        /// <param name="nomDateColonne">Nom de la colonne contenant la date</param>
        public ParserCSV(string filepath, CultureInfo culture, bool colNamed, string nomDateColonne)
            : this(filepath, culture, colNamed, nomDateColonne, false)
        { }

        /// <summary>
        /// Constructeur pour le parser d'un fichier CSV
        /// </summary>
        /// <param name="filepath">Chemin d'accès du fichier</param>
        /// <param name="culture">Culture du fichier (langue)</param>
        /// <param name="colNamed">Indique si le nom des colonnes est indiqué dans le fichier.</param>
        public ParserCSV(string filepath, CultureInfo culture, bool colNamed)
            : this(filepath, culture, colNamed, "Date")
        { }

        /// <summary>
        /// Constructeur pour le parser d'un fichier CSV
        /// </summary>
        /// <param name="filepath">Chemin d'accès du fichier</param>
        /// <param name="colNamed">Indique si le nom des colonnes est indiqué dans le fichier.</param>
        public ParserCSV(string filepath, bool colNamed)
            : this(filepath, CultureInfo.CurrentUICulture, colNamed)
        { }
        #endregion

        #region Méthodes
        /// <summary>
        /// Remplit la base de données à partir du fichier Csv importé
        /// </summary>
        /// <param name="d">base de donnée</param>
        public override void ParseFile(Data.Data d)
        {
            WcfLibrary.Constantes.displayDEBUG("start parseCSV", 2);

            //On teste le bon ordre des dates
            if (d.Fin < d.Debut)
            {
                throw new WrongDates(@"La date de fin ne peut être antérieure au début de l'acquisition");
            }

            if (String.IsNullOrEmpty(_CurrentSymbol))
                _CurrentSymbol = d.Symbol.First();

            WcfLibrary.Constantes.displayDEBUG(_Filepath, 2);
            StreamReader str = new StreamReader(_Filepath);
            string line;
            WcfLibrary.Constantes.displayDEBUG("lu ok", 2);
            // Lien entre nom colonne et numéro colonne
            Dictionary<string, int> correspondanceColonne = new Dictionary<string, int>();
            // Lien entre nom ligne (symbole + data) et numéro de ligne
            Dictionary<string, int> correspondanceLigne = new Dictionary<string, int>();

            if (_Nom || _Transpose)
            {
                line = str.ReadLine();
                string[] ligneSplitee;
                //On splite la ligne pour éliminer les séparateurs
                ligneSplitee = line.Split(new char[] { _Separateur }, StringSplitOptions.None);

                int startCol = (_Nom && _Transpose) ? 1 : 0;
                for (int i = startCol; i < ligneSplitee.Length; i++)
                {
                    string name = CleanData(ligneSplitee[i]);
                    correspondanceColonne.Add(name, i);

                    // Si transpose, alors on prépare la ligne
                    if (_Transpose)
                    {
                        DateTime t = DateTime.Parse(CleanData(name));
                        if (t >= d.Debut && t <= d.Fin)
                        {
                            // Création d'une nouvelle ligne
                            DataRow row = d.Ds.Tables[0].NewRow();
                            row["Symbol"] = _CurrentSymbol;
                            row["Date"] = DateTime.Parse(name);
                            d.Ds.Tables[0].Rows.Add(row);

                            // Ajout de la correspondance
                            int index = d.Ds.Tables[0].Rows.IndexOf(row);
                            correspondanceLigne.Add(_CurrentSymbol + name, index);
                        }
                    }
                }
            }
            else
            {
                int i = 0;
                correspondanceColonne.Add(_NomDateColonne, i);
                i++;
                foreach (var colonne in d.Columns)
                {
                    correspondanceColonne.Add(colonne, i);
                    i++;
                }
            }

            //Traitement ligne par ligne
            int numLine = 0;
            while ((line = str.ReadLine()) != null)
            {
                if (line != "")
                {
                    string[] ligneSplitee;
                    //On splite la ligne pour éliminer les séparateurs
                    ligneSplitee = line.Split(new char[] { _Separateur }, StringSplitOptions.None);

                    if (_Transpose)
                    {
                        if (_Nom)
                        {
                            // Si la colonne n'existe pas, alors on l'ajoute à la DataTable
                            string nameCol = ligneSplitee[0];
                            if(! d.Columns.Contains(nameCol))
                            {
                                d.Columns.Add(nameCol);

                                DataColumn dataColumn = new DataColumn(nameCol, System.Type.GetType("System.Double"));
                                d.Ds.Tables[0].Columns.Add(dataColumn);
                            }

                            WcfLibrary.Constantes.displayDEBUG(nameCol + " - line splited : " + ligneSplitee.Length, 3);

                            // On insère les éléments
                            foreach (var date in correspondanceColonne.Keys)
                            {
                                if (date != _NomDateColonne)
                                {
                                     DateTime t = DateTime.Parse(CleanData(date));
                                     if (t >= d.Debut && t <= d.Fin)
                                     {
                                         // On insère dans une partie de chaque ligne
                                         String value = CleanData(ligneSplitee[correspondanceColonne[date]]);
                                         WcfLibrary.Constantes.displayDEBUG(date + " " + value + " / " + correspondanceColonne[date], 4);
                                         double val = 0.0;
                                         if (value != null && value != "")
                                         {
                                            val = Double.Parse(value, _Culture);
                                         }
                                         WcfLibrary.Constantes.displayDEBUG("parse ok", 5);

                                         int index = correspondanceLigne[_CurrentSymbol + date];
                                         WcfLibrary.Constantes.displayDEBUG("insert table : " + index, 5);

                                         d.Ds.Tables[0].Rows[index][nameCol] = val;
                                         WcfLibrary.Constantes.displayDEBUG("insert table ok", 5);
                                     }
                                }
                            }
                        }
                    }
                    else
                    {
                        DateTime t = DateTime.Parse(CleanData(ligneSplitee[correspondanceColonne[_NomDateColonne]]));
                        if (t >= d.Debut && t <= d.Fin)
                        {
                            // On insère la ligne complète
                            DataRow dr = d.Ds.Tables[0].NewRow();
                            dr["Symbol"] = _CurrentSymbol;
                            dr["Date"] = t;

                            foreach (string colonne in d.Columns)
                            {
                                dr[colonne] = Double.Parse(CleanData(ligneSplitee[correspondanceColonne[colonne]]), _Culture);
                            }

                            d.Ds.Tables[0].Rows.Add(dr);
                        }
                    }
                    numLine++;
                }
            }
            str.Close();

            WcfLibrary.Constantes.displayDEBUG("end parseCSV", 2);
        }

        private string CleanData(string p)
        {
            string res = p;
            // Fully encapsulated with no comma within
            if (p.StartsWith("\"") && p.EndsWith("\""))
            {
                res = p.Substring(1, p.Length - 1);
            }
            res = res.Replace("\"\"", "\"");
            return res;
        }
        #endregion
    }
}
