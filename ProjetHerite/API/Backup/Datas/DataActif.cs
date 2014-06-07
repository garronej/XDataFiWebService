using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIFiMag.Datas
{
    /// <summary>
    /// Implémente Data dans le cas de la récupération d'actifs
    /// </summary>
    public class DataActif : Data
    {
        /// <summary>
        /// Constructeur pour recuperer les données historique d'un actif
        /// </summary>
        /// <param name="symbol">Nom des symboles à traiter</param>
        /// <param name="columns">Colonnes à stocker (high, low, ...</param>
        /// <param name="debut">Date de début</param>
        /// <param name="fin">Date de fin</param>
        public DataActif(List<string> symbol, List<HistoricalColumn> columns, DateTime debut, DateTime fin)
        {

            //On teste le bon ordre des dates
            if (fin < debut)
            {
                throw new WrongDates(@"La date de fin ne peut être antérieure au début de l'acquisition");
            }

            Type = TypeData.HistoricalData;
            Symbol = symbol;
            Columns = new List<string>();
            foreach (var item in columns)
            {
                Columns.Add(item.ToString());
            }
            Debut = debut;
            Fin = fin;
            Table = new DataSet();
        }

    }
}
