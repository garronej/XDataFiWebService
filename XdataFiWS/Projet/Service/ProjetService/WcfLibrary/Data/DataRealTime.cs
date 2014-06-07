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
    /// Implémentation de Data dans le cadre de la récupération en temps réel
    /// </summary>
    public class DataRealTime : Data
    {
        #region Attributs
        /// <summary>
        /// Fréquence d'acquisition
        /// </summary>
        public TimeSpan Periode { get; protected set; }
        /// <summary>
        /// Durée d'acquisition
        /// </summary>
        public TimeSpan Duree { get; protected set; }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur pour DataRealTime
        /// </summary>
        /// <param name="symbol">Symbole à traiter</param>
        /// <param name="duree">Durée de l'acquisition</param>
        /// <param name="periode">Période d'acquisition</param>
        public DataRealTime(string symbol, TimeSpan duree, TimeSpan periode)
        {
            Type = TypeData.RealTime;

            Symbol = new List<string>();
            Symbol.Add(symbol);

            Columns = new List<string>();
            Columns.Add("Price"); Columns.Add("Bid"); Columns.Add("Ask");

            Duree = duree;
            Periode = periode;

            initDataSet();
        }
        #endregion
    }
}
