using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Globalization;
using System.Timers;

namespace APIFiMag.Importer
{
    /// <summary>
    /// Classe de l'acquisition en temps réel
    /// </summary>
    public class ImportReal : Import
    {
        #region Attributs

        /// <summary>
        /// Déclaration du Timer
        /// </summary>
        private Timer myTimer;
        /// <summary>
        /// instance de data
        /// </summary>
        Datas.DataRealTime _data;

        #endregion

        #region Constructeur

        /// <summary>
        /// Méthode d'import
        /// </summary>
        public ImportReal()
        {
            //Initialisation du timer
            myTimer = new Timer();
            myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);

        }

        void myTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //remplissage de _data
            foreach (String s in _data.Symbol)
            {
                //Insertion dans la table de données temps réel
                DateTime t = DateTime.Now;
                double price = YQL.MonPrixTempsReel(s);
                double bid = YQL.Bid(s);
                double ask = YQL.Ask(s);

                DataSet.DataRow row = _data.Table.Data.NewDataRow();
                row.Date = t;
                row.Symbol = s;
                row.Column = "Price";
                row.Value = price;
                _data.Table.Data.AddDataRow(row);

                row = _data.Table.Data.NewDataRow();
                row.Date = t;
                row.Symbol = s;
                row.Column = "Bid";
                row.Value = bid;
                _data.Table.Data.AddDataRow(row);

                row = _data.Table.Data.NewDataRow();
                row.Date = t;
                row.Symbol = s;
                row.Column = "Ask";
                row.Value = ask;
                _data.Table.Data.AddDataRow(row);

            }
        }
        #endregion

        #region Import
        /// <summary>
        /// Implémentation de Import, cas de l'acquisiton en temps réel
        /// </summary>
        /// <param name="d">Base de donnée</param>
        public void Import(Data d)
        {

            try
            {
                System.Net.IPHostEntry Test = System.Net.Dns.GetHostEntry("www.google.fr");
            }
            catch
            {
                throw new ConnectivityException(@"Il semble que votre connection réseau soit inactive ou qu'elle ne fonctionne pas correctement, veuillez vérifier vos paramètres de connexions ou contacter votre administrateur système");
            }

            _data = (Datas.DataRealTime)d;
            myTimer.Interval = _data.Periode.TotalMilliseconds;
            //démarrage du timer
            myTimer.Start();
            //mise en pause du thread le temps que les opérations soient finies
            System.Threading.Thread.Sleep(_data.Duree);
            myTimer.Stop();
        }
        #endregion
    }
}