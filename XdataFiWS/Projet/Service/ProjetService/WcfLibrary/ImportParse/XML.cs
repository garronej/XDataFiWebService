using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfLibrary.ImportParse
{
    public class XML : ImportParse
    {
        string s;
        string sSchema;

        #region Constructeur
        /// <summary>
        /// Méthode de connexion
        /// </summary>
        public XML(string s, string sSchema)
        {
            this.s = s;
            this.sSchema = sSchema;
            _Parser = new Parser.ParserXML();
        }
        #endregion

        #region Méthodes
        public void setStr(string s, string sSchema)
        {
            this.s = s;
            this.sSchema = sSchema;
        }
        /// <summary>
        /// Crée les fichiers
        /// et le parse, en remplissant les données
        /// </summary>
        /// <param name="d">Base de donnée, DataXML </param>
        public override void ImportAndParse(Data.Data d)
        {
            // Création des fichiers
            string filePath = "xml.xml";
            string filePathSchema = "xmlSchema.xsd";

            // Si les fichiers existe,nt alors on les nomme autrement
            int i = 0;
            string nameFile = filePath;
            string nameFileSchema = filePathSchema;
            while (System.IO.File.Exists(@nameFile))
            {
                i++;
                nameFile = i + "_" + filePath;
                nameFileSchema = i + "_" + filePathSchema;
            }
            filePath = nameFile;
            filePathSchema = nameFileSchema;

            // Fichier XML
            StreamWriter stw = new StreamWriter(filePath);
            stw.Write(s);
            stw.Close();
            WcfLibrary.Constantes.displayDEBUG("Fichier XML créé", 1);

            // Fichier XSD
            StreamWriter stwSchema = new StreamWriter(filePathSchema);
            stwSchema.Write(sSchema);
            stwSchema.Close();
            WcfLibrary.Constantes.displayDEBUG("Fichier XSD créé", 1);

            // Parse
            _Parser = new Parser.ParserXML(filePath, filePathSchema);
            _Parser.ParseFile(d);

            // On supprime les fichiers
            System.IO.File.Delete(@filePath);
            System.IO.File.Delete(@filePathSchema);
        }
        #endregion

    }
}
