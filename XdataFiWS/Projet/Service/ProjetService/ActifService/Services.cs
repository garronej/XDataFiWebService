using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    /// <summary>
    /// Contient toutes les fonctions de tous les services disponibles
    /// </summary>
    public class Services : IActifService, IExchangeRateService, IInterestRateService
    {
        #region IActifService
        /// <summary>
        /// Crée un DataActif, et le remplie de l'historique des actifs
        /// </summary>
        /// <param name="symbol">Nom des symboles à traiter</param>
        /// <param name="colums">Informations à fournir (high, low, ...)</param>
        /// <param name="debut">Date de début</param>
        /// <param name="fin">Date de fin</param>
        public WcfLibrary.Data.DataActif getActifHistorique(List<string> symbol, List<WcfLibrary.Data.Data.HistoricalColumn> columns, DateTime debut, DateTime fin)
        {
            //RemoteEndpointMessageProperty messageProperty = OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            
            //Console.WriteLine("Remote address is: {0}", messageProperty.Address);
            //Service1 serv = new Service1();
            //serv.DoWork();

            WcfLibrary.Constantes.displayDEBUG("start getActifHistorique", 0);
            
            // Création du DataActif
            WcfLibrary.Data.DataActif d = new WcfLibrary.Data.DataActif(symbol, columns, debut, fin);

            // Import des données désirées
            WcfLibrary.ImportParse.Yahoo i = new WcfLibrary.ImportParse.Yahoo();
            i.ImportAndParse(d);

            WcfLibrary.Constantes.displayDEBUG("end getActifHistorique", 0);

            return d;
        }
        #endregion

        #region IExchangeRateService
        /// <summary>
        /// Recherche les taux de change suivant les données en paramètre
        /// </summary>
        /// <param name="symbol">Nom de la monnaie étalon</param>
        /// <param name="columns">Listes des monnaies à comparer à la monnaie étalon</param>
        /// <param name="debut">Date de début de sauvegarde des données</param>
        /// <param name="fin">Date de fin</param>
        /// <param name="freq">Fréquence de sauvegarde, traitement différent si journalier ou mensuel/annuel</param>
        public WcfLibrary.Data.DataExchangeRate getExchangeRate(
            WcfLibrary.Data.Data.Currency symbol, List<WcfLibrary.Data.Data.Currency> columns,
            DateTime debut, DateTime fin, WcfLibrary.Data.Data.Frequency freq)
        {
            WcfLibrary.Constantes.displayDEBUG("start getExchangeRate", 0);

            // Création du DataIRate
            WcfLibrary.Data.DataExchangeRate d = new WcfLibrary.Data.DataExchangeRate(symbol, columns, debut, fin, freq);

            // Import des données désirées
            WcfLibrary.ImportParse.FXTop i = new WcfLibrary.ImportParse.FXTop();
            i.ImportAndParse(d);

            WcfLibrary.Constantes.displayDEBUG("end getExchangeRate", 0);

            return d;
        }
        #endregion

        #region IInterestRateService
        /// <summary>
        /// Recherche les taux d'intérêts suivant les données en paramètre
        /// </summary>
        /// <param name="symbol">Nom du symbol à traiter</param>
        /// <param name="debut">Date de début</param>
        /// <param name="fin">Date de fin</param>
        public WcfLibrary.Data.DataInterestRate getInterestRate(WcfLibrary.Data.Data.InterestRate symbol, DateTime debut, DateTime fin)
        {
            WcfLibrary.Constantes.displayDEBUG("start getInterestRate", 0);
            
            // Création du DataIRate
            WcfLibrary.Data.DataInterestRate d = new WcfLibrary.Data.DataInterestRate(symbol, debut, fin);

            // Import des données désirées
            WcfLibrary.ImportParse.EBF i = new WcfLibrary.ImportParse.EBF();
            i.ImportAndParse(d);

            WcfLibrary.Constantes.displayDEBUG("end getInterestRate", 0);

            return d;
        }
        #endregion    
    }
}
