using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIFiMag.Datas
{   
    /// <summary>
    /// Implémentation de Data dans le cas des tauxs d'intérêts
    /// </summary>
    public class DataIRate : Data
    {
                /// <summary>
        /// Constructeur pour taux d'intérêts
        /// </summary>
        /// <param name="symbol">Symbole à traiter</param>
        /// <param name="debut">Date de début</param>
        /// <param name="fin">Date de fin</param>
        public DataIRate(InterestRate symbol, DateTime debut, DateTime fin)
        {

            //On teste le bon ordre des dates
            if (fin < debut)
            {
                throw new WrongDates(@"La date de fin ne peut être antérieure au début de l'acquisition");
            }

            Type = TypeData.InterestRate;
            Symbol = new List<string>();
            Symbol.Add(symbol.ToString());
            Columns = new List<string>();

            //Ajout des Symboles suivant le type de contrat
            switch (symbol)
            {
                case InterestRate.EURIBOR:
                    Columns.Add("1w");
                    Columns.Add("2w");
                    Columns.Add("3w");
                    Columns.Add("1m");
                    Columns.Add("2m");
                    Columns.Add("3m");
                    Columns.Add("4m");
                    Columns.Add("5m");
                    Columns.Add("6m");
                    Columns.Add("7m");
                    Columns.Add("8m");
                    Columns.Add("9m");
                    Columns.Add("10m");
                    Columns.Add("11m");
                    Columns.Add("12m");
                    break;
                case InterestRate.USDEURIBOR:
                    Columns.Add("ON");
                    Columns.Add("1w");
                    Columns.Add("2w");
                    Columns.Add("1m");
                    Columns.Add("2m");
                    Columns.Add("3m");
                    Columns.Add("4m");
                    Columns.Add("5m");
                    Columns.Add("6m");
                    Columns.Add("7m");
                    Columns.Add("8m");
                    Columns.Add("9m");
                    Columns.Add("10m");
                    Columns.Add("11m");
                    Columns.Add("12m");
                    break;
                case InterestRate.EONIA:
                    Columns.Add(InterestRate.EONIA.ToString());
                    break;
                case InterestRate.EUREPO:
                    Columns.Add("tn");
                    Columns.Add("1w");
                    Columns.Add("2w");
                    Columns.Add("3w");
                    Columns.Add("1m");
                    Columns.Add("2m");
                    Columns.Add("3m");
                    Columns.Add("6m");
                    Columns.Add("9m");
                    Columns.Add("12m");
                    break;
                case InterestRate.EONIASWAP:
                    Columns.Add("1w");
                    Columns.Add("2w");
                    Columns.Add("3w");
                    Columns.Add("1m");
                    Columns.Add("2m");
                    Columns.Add("3m");
                    Columns.Add("4m");
                    Columns.Add("5m");
                    Columns.Add("6m");
                    Columns.Add("7m");
                    Columns.Add("8m");
                    Columns.Add("9m");
                    Columns.Add("10m");
                    Columns.Add("11m");
                    Columns.Add("12m");
                    Columns.Add("15m");
                    Columns.Add("18m");
                    Columns.Add("21m");
                    Columns.Add("24m");
                    break;
                default:
                    break;
            }
            Debut = debut;
            Fin = fin;
            Table = new DataSet();
        }
    }
}
