using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfLibrary.Data
{
    [DataContract]
    /// <summary>
    /// Implémente Data dans le cas de la récupération des taux de change depuis FXTop
    /// </summary>
    public class DataExchangeRate : Data
    {
        /// <summary>
        /// Fréquence d'acquisition (d,m,y), ne sert que dans le cas de l'acquisition fxtop 
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
        public DataExchangeRate(Currency symbol, List<Currency> columns, DateTime debut, DateTime fin, Frequency freq)
        {
            //On teste le bon ordre des dates
            if (fin < debut)
            {
                throw new WrongDates(@"La date de fin ne peut être antérieure au début de l'acquisition");
            }
            
            Type = TypeData.ExchangeRate;

            Symbol = new List<string>();
            Columns = new List<string>();

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
                Columns.Add("Average");
                Columns.Add("Min");
                Columns.Add("Max");
            }

            Debut = debut;
            Fin = fin;
            Freq = freq;

            initDataSet();
        }
    }
}
