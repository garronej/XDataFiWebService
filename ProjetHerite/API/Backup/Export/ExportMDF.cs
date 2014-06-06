using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace APIFiMag.Exporter
{
    public class ExportMDF : Export
    {
        private string _FilePath;

        public ExportMDF(string filepath)
        {
            _FilePath = filepath;
        }

        public void Export(Data d)
        {

            //récupération du fichier MDF embarqué dans la DLL de l'API
            System.Reflection.Assembly thisExe;
            thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            if (thisExe == null)
                throw new ErreurInterne("thisExe null", true);
            Console.WriteLine(thisExe.GetName());
            System.IO.Stream fileIn = null;
            string filename = "back.mdf";
            fileIn = thisExe.GetManifestResourceStream("APIFiMag.back.mdf");


            if (fileIn == null)
                throw new ErreurInterne("Impossible de localiser la ressource implantée '" + filename + "' dans l'assembly '" + thisExe.GetName().Name + "'", true);

            //Création d'un stream pour pouvoir remplir le fichier MDF
            if (System.IO.File.Exists(@_FilePath))
            {
                //le fichier existe : on le supprime
                System.IO.File.Delete(@_FilePath);
            }

            System.IO.Stream fileOut = System.IO.File.OpenWrite(@_FilePath);
            if (fileOut == null)
                throw new ErreurInterne("Impossible d'ouvrir le fichier de sortie", true);

            //Copie du fichier MDF embarqué dans la DLL dans le nouveau fichier
            const int SIZE_BUFF = 1024;
            byte[] buffer = new Byte[SIZE_BUFF];
            int bytesRead;
            while ((bytesRead = fileIn.Read(buffer, 0, SIZE_BUFF)) > 0)
            {
                fileOut.Write(buffer, 0, bytesRead);
            }
            //fermeture du fichier destination a la fin de la copie
            fileOut.Close();
            fileOut.Dispose();

            //création de la chaîne de connexion vers le serveur SQL associé au fichier MDF que l'on vient de créer
            string ConString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\"+ _FilePath + ";Integrated Security=True;User Instance=True";
            //connexion à ce serveur SQL
            using (System.Data.SqlClient.SqlConnection con = new SqlConnection(ConString))
            {
                //vérification que la table n'existe pas déjà dans la base de données
                //SqlCommand cmd = new SqlCommand("SELECT TABLE_NAME FROM   INFORMATION_SCHEMA.TABLES WHERE Table_Type='BASE TABLE'", con);
                con.Open();
                //SqlDataReader res = cmd.ExecuteReader();
                //bool trouve = false;
                //while (res.Read())
                //{
                //    if (String.Equals(string.Format("{0}", res[0]), "Historique"))
                //    {
                //        trouve = true;
                //        break;
                //    }
                //}
                //con.Close();
                //con.Open();
                //if (trouve)
                //{
                //    //si la base existe : on la supprime
                //    cmd = new SqlCommand("DROP TABLE HISTORIQUE", con);
                //    cmd.ExecuteScalar();
                //}

                //création d'une nouvelle table Historical suivant le schémas de la table Historical dans le DataSet
                SqlTableCreator create = new SqlTableCreator(con, null);
                create.CreateFromDataTable();

                //copie en bloc de la table Historical du dataset dans la table Historical du fichier MDF
                SqlBulkCopy copy = new SqlBulkCopy(con);
                copy.DestinationTableName = "Historique";
                copy.WriteToServer(d.Table.Data);
                con.Close();

                //fermeture du pool de connexion associé la connexion que l'on vient d'utiliser 
                SqlConnection.ClearPool(con);
            }
        }
    }
}
