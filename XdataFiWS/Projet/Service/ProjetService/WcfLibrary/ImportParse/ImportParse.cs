using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WcfLibrary.ImportParse
{
    /// <summary>
    /// Classe abstraite, permettant de récupérer des données depuis le web
    /// </summary>
    public abstract class ImportParse
    {
        /// <summary> Parser </summary>
        protected Parser.Parser _Parser;
        /// <summary> Chemin du fichier </summary>
        protected string _Filepath;


        /// <summary>
        /// Télécharge le fichier désiré
        /// et la parse, en remplissant les données
        /// </summary>
        public abstract void ImportAndParse(Data.Data d);

        /// <summary>
        /// Télécharge le fichier depuis l'url et l'enregistre suivant _Filepath
        /// </summary>
        /// <param name="siteUri">lien url du fichier à télécharger</param>
        protected void ImportFile(Uri siteUri)
        {
            WcfLibrary.Constantes.displayDEBUG(_Filepath, 1);

            // Si le fichier existe, alors on le supprime
            if (System.IO.File.Exists(@_Filepath))
            {
                System.IO.File.Delete(@_Filepath);
            }

            // Téléchargement
            WcfLibrary.Constantes.displayDEBUG("start Download", 2);
            WebClient client = new WebClient();
            client.DownloadFile(siteUri, _Filepath);
            WcfLibrary.Constantes.displayDEBUG("end Download", 2);

            WcfLibrary.Constantes.displayDEBUG("Le fichier a bien été créé", 1);
        }
    }
}
