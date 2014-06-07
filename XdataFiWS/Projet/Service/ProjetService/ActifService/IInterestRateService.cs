using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Service
{
    [ServiceContract]
    /// <summary> Service des taux d'intérêts </summary>
    public interface IInterestRateService
    {
        [OperationContract]
        /// <summary>
        /// Recherche les taux d'intérêts suivant les données en paramètre
        /// </summary>
        /// <param name="symbol">Nom du symbol à traiter</param>
        /// <param name="debut">Date de début</param>
        /// <param name="fin">Date de fin</param>
        WcfLibrary.Data.DataInterestRate getInterestRate(WcfLibrary.Data.Data.InterestRate symbol, DateTime debut, DateTime fin);
    }
}
