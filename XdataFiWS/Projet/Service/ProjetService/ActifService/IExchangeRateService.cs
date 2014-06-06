using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceContract]
    /// <summary> Service des taux de change </summary>
    public interface IExchangeRateService
    {
        [OperationContract]
        /// <summary>
        /// Recherche les taux de change suivant les données en paramètre
        /// </summary>
        /// <param name="symbol">Nom de la monnaie étalon</param>
        /// <param name="columns">Listes des monnaies à comparer à la monnaie étalon</param>
        /// <param name="debut">Date de début de sauvegarde des données</param>
        /// <param name="fin">Date de fin</param>
        /// <param name="freq">Fréquence de sauvegarde, traitement différent si journalier ou mensuel/annuel</param>
        WcfLibrary.Data.DataExchangeRate getExchangeRate(
            WcfLibrary.Data.Data.Currency symbol, List<WcfLibrary.Data.Data.Currency> columns,
            DateTime debut, DateTime fin, WcfLibrary.Data.Data.Frequency freq);
    }
}
