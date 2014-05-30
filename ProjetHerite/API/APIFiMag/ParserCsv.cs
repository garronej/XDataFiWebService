using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace APIFiMag
{
    public class ParserCsv : Parser
    {   
        /// <summary>
        /// Chemin du fichier à importer
        /// </summary>
        private string _Filepath;

        /// <summary>
        /// Séparateur voulu, suivant la norme du fichier Csv
        /// </summary>
        private char _Separateur;

        /// <summary>
        /// Vaut true si l'ordre date, symbole 1, ... est respecté, false sinon
        /// </summary>
        private bool _Correspondance;

        /// <summary>
        /// Vaut 0 par défaut, sinon vaut le numéro de la colonne date si _Correspondance = false
        /// </summary>
        private int _CompteurDate;

        /// <summary>
        /// _TableauCorrespondance[i] vaut i si _Correspondance, vaut la valeur voulue sinon
        /// </summary>
        private int[] _TableauCorrespondance;

        /// <summary>
        /// Constructeur simple, _Correspondance = true
        /// <param name="filepath">chemin du fichier</param>
        /// <param name="nombreSymb">nombre de Symboles </param>
        /// <param name="separateur">séparateur voulu</param>
        /// </summary>
        public ParserCsv(string filepath, int nombreSymb, char separateur)
        {
            _Filepath = filepath;
            _Correspondance = true;
            _CompteurDate = 0;
            _TableauCorrespondance = new int[nombreSymb+1];
            _Separateur = separateur;
            for (int i = 0; i <= nombreSymb; i++)
                _TableauCorrespondance[i] = i;
        }
        /// <summary>
        /// Constructeur avec permutations des symboles et de la date
        /// </summary>
        /// <param name="filepath">chemin du fichier</param>
        /// <param name="tabPermut">tableau des permutations</param>
        /// <param name="separateur">séparateur voulu</param>
        /// <param name="compteurDate">indice de la date</param>
        public ParserCsv(string filepath, int[] tabPermut, int compteurDate, char separateur)
        {
            _Filepath = filepath;
            _Correspondance = false;
            _CompteurDate = compteurDate;
            _Separateur = separateur;
            _TableauCorrespondance = new int[tabPermut.Length];
            Array.Copy(tabPermut, _TableauCorrespondance, tabPermut.Length);
        }

        /// <summary>
        /// Remplit la base de données à partir du fichier Csv importé
        /// </summary>
        /// <param name="d">base de donnée</param>
        /// <param name="separateur">séparateur voulu, "," ou ";"</param>
        public override void Import(Data d)
        {
            StreamReader str = new StreamReader(_Filepath);
            string line;
            string donnee = "";
            bool cont = false;
            DateTime date = DateTime.MinValue;
            string x;
            double[] values = new double[d.Symbols.Count];
            //on ne traite que les symboles voulues, les suivants sont ignorés
            //int compteur = 0;

            //On connait la correspondance, la première ligne ne sert à rien
            if (_Correspondance)
                line = str.ReadLine();


            //Traitement ligne par ligne
            while ((line = str.ReadLine()) != null)
            {
             
                int compteur = 0;


                string[] ligneSplitee;

                //On splite la ligne pour éliminer les séparateurs
                ligneSplitee = line.Split(new char[] { _Separateur }, StringSplitOptions.None);

                //On cherche la date tout d'abord
                x = ligneSplitee[_CompteurDate];
                if (x.StartsWith("\"") && x.EndsWith("\""))
                    x = x.Substring(1, x.Length - 1);
                date = DateTime.Parse(x);

                foreach (var y in ligneSplitee)
                {
                    //déjà vu tous les symboles voulus
                    if (compteur == d.Symbols.Count + 1) break;

                    x = y;

                    //Besoin de traiter les "" le cas échéant
                    if (cont)
                    {
                        //End of field
                        if (x.EndsWith("\""))
                        {
                            donnee += "." + x.Substring(0, x.Length - 1);
                            d.Table.Data.AddDataRow(date, d.Symbols[_TableauCorrespondance[compteur]], Double.Parse(x));
                            donnee = "";
                            cont = false;
                            compteur++;
                            continue;
                        }
                        else
                        {
                            //Field Not ended
                            donnee += "." + x;
                            continue;
                        }
                    }


                    // Fully encapsulated with no comma within
                    if (x.StartsWith("\"") && x.EndsWith("\""))
                    {
                        if ((x.EndsWith("\"\"") && !x.EndsWith("\"\"\"")) && x != "\"\"")
                        {
                            cont = true;
                            donnee = x;
                            continue;
                        }

                        donnee = x.Substring(1, x.Length - 1);

                        d.Table.Data.AddDataRow(date, d.Symbols[_TableauCorrespondance[compteur]], Double.Parse(x));
                        compteur++;
                    }

                    // Start of encapsulation but comma has split it into at least next field
                    if (x.StartsWith("\"") && !x.EndsWith("\""))
                    {
                        cont = true;
                        donnee += "\"";
                        continue;
                    }
                    // Non encapsulated complete field
                    if (compteur != _CompteurDate)
                        d.Table.Data.AddDataRow(date, d.Symbols[_TableauCorrespondance[compteur]-1], Double.Parse(x));
                    compteur++;


                }

            }
        }

    }

}
