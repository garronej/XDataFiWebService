using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;

namespace APIFiMag
{
    /// <summary>
    /// Classe nécessaire à l'affichage des informations statiques sur les sociétés
    /// </summary>
    public class InfoStat
    {

        #region Attributs
        /// <summary>
        /// liste des symboles (utile pour le thread)
        /// </summary>
        public List<string> symm;
        /// <summary>
        /// tableau des entreprises (sous forme d'InfosStatiques)
        /// </summary>
        public InfosStatiques[] firms;
        /// <summary>
        /// Indique si le téléchargement est terminé
        /// </summary>
        public bool endDownload { get; set; }
        /// <summary>
        /// Semaphore pour la synchronisation 
        /// </summary>
        private Semaphore sem = new Semaphore(0, 1);
        #endregion

        #region Variables Globales
        /// <summary>
        /// avancement courant de la barre de progression eventuelle
        /// </summary>
        public static int avancement { get; set; }
        /// <summary>
        /// avancement total de la barre de progression eventuelle 
        /// </summary>
        public static int total { get; set; }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur de la classe InfoStat
        /// </summary>
        /// <param name="symbols">liste des symboles dont on veut obtenir les informations statiques</param>
        public InfoStat(List<string> symbols)
        {
            symm = symbols;
            firms = new InfosStatiques[symm.Count()];
        }

        #endregion

        #region Affichage des données

        public void telechargerInfoStat(bool thread = false)
        {
            try
            {
                if (thread)
                {
                    Thread th = new Thread(new ThreadStart(launchDownloadInfoStat));
                    th.Start();
                }
                else
                {
                    launchDownloadInfoStat();
                }
            }
            catch (ErreurSource e)
            {
                throw (new ErreurInterne("Dans infos stats, thread"));
            }
            catch
            {
                throw (new ErreurInterne("Dans infos stats, thread"));
            }
        }

        private void launchDownloadInfoStat()
        {
             lock (this)
            {
                try
                {
                    avancement = 0;
                    endDownload = false;
                    total = symm.Count();

                    // variable permettant de parcourir firms
                    int i = 0;

                    foreach (string symbol in symm)
                    {
                        try
                        {
                            firms[i] = new InfosStatiques(symbol);
                        }
                        catch
                        {
                            throw (new ErreurInterne("Dans infos stats, download"));
                        }
                        avancement++;
                        i++;
                    }
                    endDownload = true;
                }
                catch
                {
                    endDownload = true;
                }

                finally
                {
                    sem.Release();
                }
            }
        }
            public void wait()
            {
                sem.WaitOne();
            }
        public void release(){
            sem.Release();
        }
        

        #endregion
    }
}
