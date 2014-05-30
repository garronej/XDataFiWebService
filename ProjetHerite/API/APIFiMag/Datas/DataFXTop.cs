using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIFiMag.Datas
{   
    /// <summary>
    /// Implémente Data dans le cas de la récupération des taux de change depuis FXTop
    /// </summary>
    public class DataFXTop : Data
    {
        /// <summary>
        /// Fréquence d'acquisition (d,m y), ne sert que dans le cas de l'acquisition fxtop 
        /// </summary>
        public Frequency Freq { get; private set; }

                /// <summary>
        /// Constructeur pour recuperer les taux de change
        /// </summary>
        /// <param name="symbol">Nom de la monnaie étalon</param>
        /// <param name="columns">Listes des monnaies à comparer à la monnaie étalon</param>
        /// <param name="debut">Date de début de sauvegarde des données</param>
        /// <param name="fin">Date de fin</param>
        /// <param name="freq">Fréquence de sauvegarde, traitement différent si journalier ou mensuel/annuel</param>
        public DataFXTop(Currency symbol, List<Currency> columns, DateTime debut, DateTime fin, Frequency freq)
        {
            Type = TypeData.Currency;
            Symbol = new List<string>();
            Columns = new List<string>();

            //On teste le bon ordre des dates
            if (fin < debut)
            {
                throw new WrongDates(@"La date de fin ne peut être antérieure au début de l'acquisition");
            }

            //si Journalier, les colonnes correspondent à chaque monnaie à comparer
            if (freq == Frequency.Daily)
            {
                Symbol.Add(symbol.ToString());
                foreach (var item in columns)
                {
                    if (item != symbol)
                        Columns.Add(symbol.ToString() + "/" + item.ToString());
                }

            }
            //Si Mensuel/Annuel, chaque symbole correspond à un couple de monnaie, les colonnes correspondant aux valeurs moyennes, minimales et maximales
            else
            {
                foreach (var item in columns)
                {
                    if (item != symbol)
                        Symbol.Add(symbol.ToString() + "/" + item.ToString());
                }
                Columns.Add("AVERAGE");
                Columns.Add("MIN");
                Columns.Add("MAX");
            }
            Debut = debut;
            Fin = fin;
            Freq = freq;
            Table = new DataSet();
        }
    }
}
