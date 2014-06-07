using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceContract]
    /// <summary> Service de l'historique des actifs </summary>
    public interface IActifService
    {
        [OperationContract]
        /// <summary>
        /// Crée un DataActif, et le remplie de l'historique des actifs
        /// </summary>
        /// <param name="symbol">Nom des symboles à traiter</param>
        /// <param name="colums">Informations à fournir (high, low, ...)</param>
        /// <param name="debut">Date de début</param>
        /// <param name="fin">Date de fin</param>
        WcfLibrary.Data.DataActif getActifHistorique(List<string> symbol, List<WcfLibrary.Data.Data.HistoricalColumn> columns, DateTime debut, DateTime fin);
    }
}
