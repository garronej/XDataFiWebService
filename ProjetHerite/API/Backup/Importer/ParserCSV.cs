using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
namespace APIFiMag.Importer
{   
    /// <summary>
    /// Parser pour les fichiers CSV
    /// </summary>
	public class ParserCSV : Import
	{
		#region attributs
		/// <summary>
		/// Chemin du fichier à importer
		/// </summary>
		protected string _Filepath;

		/// <summary>
		/// Séparateur voulu, suivant la norme du fichier Csv
		/// </summary>
		private char _Separateur;

		///// <summary>
		///// Vaut true si l'ordre date, symbole 1, ... est respecté, false sinon
		///// </summary>
		//private bool _Correspondance;

		/// <summary>
		/// Vaut true si le nom des colonnes est présent
		/// </summary>
		private bool _Nom;

		/// <summary>
		/// Correspond à la culture du fichier importé. le format varie en fonction de la langue utilisée
		/// </summary>
		private CultureInfo _Culture;
		private string _NomDateColonne;
		private bool _Transpose;

		protected string _CurrentSymbol;

		///// <summary>
		///// Vaut 0 par défaut, sinon vaut le numéro de la colonne date si _Correspondance = false
		///// </summary>
		//private int _CompteurDate;

		///// <summary>
		///// _TableauCorrespondance[i] vaut i si _Correspondance, vaut la valeur voulue sinon
		///// </summary>
		//private int[] _TableauCorrespondance;

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

		#region Import
		/// <summary>
		/// Remplit la base de données à partir du fichier Csv importé
		/// </summary>
		/// <param name="d">base de donnée</param>
		public virtual void Import(Data d)
		{
			//On teste le bon ordre des dates
			if (d.Fin < d.Debut)
			{
				throw new WrongDates(@"La date de fin ne peut être antérieure au début de l'acquisition");
			}

			if (String.IsNullOrEmpty(_CurrentSymbol))
				_CurrentSymbol = d.Symbol.First();
			StreamReader str = new StreamReader(_Filepath);
			string line;

			Dictionary<string, int> correspondanceColonne = new Dictionary<string, int>();
			if (_Nom || _Transpose)
			{
				line = str.ReadLine();
				string[] ligneSplitee;
				//On splite la ligne pour éliminer les séparateurs
				ligneSplitee = line.Split(new char[] { _Separateur }, StringSplitOptions.None);
				for (int i = 0; i < ligneSplitee.Length; i++)
				{
					correspondanceColonne.Add(CleanData(ligneSplitee[i]), i);
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
			while ((line = str.ReadLine()) != null)
			{
				string[] ligneSplitee;
				//On splite la ligne pour éliminer les séparateurs
				ligneSplitee = line.Split(new char[] { _Separateur }, StringSplitOptions.None);

				if (_Transpose)
				{
					foreach (var date in correspondanceColonne.Keys)
					{
						if (date != _NomDateColonne)
						{
							DateTime t = DateTime.Parse(CleanData(date));
							if (t >= d.Debut && t <= d.Fin)
							{
								DataSet.DataRow row = d.Table.Data.NewDataRow();
								row.Date = DateTime.Parse(CleanData(date));
								row.Symbol = _CurrentSymbol;
								row.Column = ligneSplitee[correspondanceColonne[_NomDateColonne]];
								row.Value = Double.Parse(CleanData(ligneSplitee[correspondanceColonne[date]]), _Culture);
								// test la positivité du prix
								if (row.Value < 0)
								{
									if (d.Type != TypeData.InterestRate)
										throw new NegativeValueImpossible(@"Un prix ne peut être négatif");
								}
								d.Table.Data.AddDataRow(row);
							}
						}
					}
				}
				else
				{
					DateTime t = DateTime.Parse(CleanData(ligneSplitee[correspondanceColonne[_NomDateColonne]]));
					if (t >= d.Debut && t <= d.Fin)
					{
						foreach (string colonne in d.Columns)
						{
							DataSet.DataRow row = d.Table.Data.NewDataRow();
							row.Date = t;
							row.Symbol = _CurrentSymbol;
							row.Column = colonne;
							row.Value = Double.Parse(CleanData(ligneSplitee[correspondanceColonne[colonne]]), _Culture);
							d.Table.Data.AddDataRow(row);
						}
					}
				}
			}
			str.Close();



			/*
			string donnee = "";
			bool cont = false;
			DateTime date = DateTime.MinValue;
			string x;
			double[] values = new double[d.Columns.Count];
			
				//on ne traite que les symboles voulues, les suivants sont ignorés
				//int compteur = 0;

				//Si on connait la correspondance, la première ligne ne sert à rien
				if (_Correspondance)
					line = str.ReadLine();


				//Traitement ligne par ligne
				while ((line = str.ReadLine()) != null)
				{
					DataSet.DataRow row = d.Table.Data.NewDataRow();
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
						if (compteur == d.Columns.Count + 1) break;

						x = y;

						//Besoin de traiter les "" le cas échéant
						if (cont)
						{
							//End of field
							if (x.EndsWith("\""))
							{
								donnee += "." + x.Substring(0, x.Length - 1);
								d.Table.Data.AddDataRow(date, d.Columns[_TableauCorrespondance[compteur]], Double.Parse(x));
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

							d.Table.Data.AddDataRow(date, d.Columns[_TableauCorrespondance[compteur]], Double.Parse(x));
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
							d.Table.Data.AddDataRow(date, d.Columns[_TableauCorrespondance[compteur] - 1], Double.Parse(x));
						compteur++;


					}

				}*/
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
