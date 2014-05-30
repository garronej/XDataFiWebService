using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIFiMag.Datas
{   
    /// <summary>
    /// Implémentation de Data dans le cadre de la récupération en temps réel
    /// </summary>
    public class DataRealTime : Data
    {
        /// <summary>
        /// Fréquence d'acquisition
        /// </summary>
        public TimeSpan Periode { get; protected set; }
        /// <summary>
        /// Durée d'acquisition
        /// </summary>
        public TimeSpan Duree { get; protected set; }

        
        /// <summary>
        /// Constructeur pour DataRealTime
        /// </summary>
        /// <param name="symbol">Symbole à traiter</param>
        /// <param name="duree">Durée de l'acquisition</param>
        /// <param name="periode">Période d'acquisition</param>
        public DataRealTime(string symbol, TimeSpan duree, TimeSpan periode)
        {
            List<string> liste = new List<string>();
            liste.Add(symbol);
            Type = TypeData.RealTime;
            Symbol = liste;
            Columns = new List<string>();
            Columns.Add("Price"); Columns.Add("Bid"); Columns.Add("Ask");
            Table = new DataSet();
            Duree = duree;
            Periode = periode;
        }


    }
}
