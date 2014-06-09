using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Test.ServiceReference;

namespace Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            testHist();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            testInterestRate();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            testExchangeRate();
        }

        protected void testHist()
        {
            #region Recuperation des Donnees

            List<string> l = new List<string>();
            //TextBox1.Text
            l.Add("CA.PA");
            l.Add("BNP.PA");

            List<Data.HistoricalColumn> columns = new List<Data.HistoricalColumn>();
            columns.Add(Data.HistoricalColumn.Open);
            columns.Add(Data.HistoricalColumn.High);
            columns.Add(Data.HistoricalColumn.Low);
            columns.Add(Data.HistoricalColumn.Close);

            DateTime debut = new DateTime(2013, 11, 30);
            DateTime fin = new DateTime(2014, 6, 3);

            ServiceReference.ActifServiceClient client = new ServiceReference.ActifServiceClient();
            DataActif d = client.getActifHistorique(l, columns, debut, fin);
            client.Close();

            //Label1.Text = d.Dict["CA.PA"].Dict[fin][0].ToString() + " "
            //            + d.Dict["BNP.PA"].Dict[fin][0].ToString();
            Label1.Text = d.Ds.Tables[0].Rows[0]["Symbol"].ToString();
            #endregion

            #region Affichage
            // On affiche la barre de titre
            // On crée une nouvelle ligne
            TableRow tRowTitle = new TableRow();
            Table1.Rows.Add(tRowTitle);

            // Nom de l'actif
            TableCell tCellNameTitle = new TableCell();
            tCellNameTitle.Text = "Symbol";
            tRowTitle.Cells.Add(tCellNameTitle);

            // Date
            TableCell tCellDateTitle = new TableCell();
            tCellDateTitle.Text = "Date";
            tRowTitle.Cells.Add(tCellDateTitle);

            foreach (string col in d.Columns)
            {
                // Ajout d'une case (correspondant à une valeur)
                TableCell tCellVal = new TableCell();
                tCellVal.Text = col;
                tRowTitle.Cells.Add(tCellVal);
            }

            // Ajout des différentes lignes
            int n = d.Ds.Tables[0].Rows.Count;

            for (int i = 0; i < n; i++)
            {
                string name = (string)d.Ds.Tables[0].Rows[i]["Symbol"];
                DateTime time = (DateTime)d.Ds.Tables[0].Rows[i]["Date"];

                // On crée une nouvelle ligne
                TableRow tRow = new TableRow();
                Table1.Rows.Add(tRow);

                // Nom de l'actif
                TableCell tCellName = new TableCell();
                tCellName.Text = name;
                tRow.Cells.Add(tCellName);

                // Date
                TableCell tCellDate = new TableCell();
                tCellDate.Text = time.ToString("dd/MM/yyyy");
                tRow.Cells.Add(tCellDate);

                foreach (string s in d.Columns)
                {
                    // Ajout d'une case (correspondant à une valeur)
                    TableCell tCellVal = new TableCell();
                    tCellVal.Text = d.Ds.Tables[0].Rows[i][s].ToString();
                    tRow.Cells.Add(tCellVal);
                }
            }
            #endregion
        }

        protected void testInterestRate()
        {
            #region Recuperation des Donnees

            DateTime debut = new DateTime(2013, 11, 10);
            DateTime fin = new DateTime(2014, 6, 3);

            ServiceReference.InterestRateServiceClient client = new ServiceReference.InterestRateServiceClient();
            DataInterestRate d = client.getInterestRate(Data.InterestRate.EURIBOR, debut, fin);
            client.Close();

            //Label1.Text = d.Dict["CA.PA"].Dict[fin][0].ToString() + " "
            //            + d.Dict["BNP.PA"].Dict[fin][0].ToString();
            Label1.Text = d.Ds.Tables[0].Rows[0]["Symbol"].ToString();
            #endregion
            
            #region Affichage
            // On affiche la bar de titre
            // On crée une nouvelle ligne
            TableRow tRowTitle = new TableRow();
            Table1.Rows.Add(tRowTitle);

            // Nom de l'actif
            TableCell tCellNameTitle = new TableCell();
            tCellNameTitle.Text = "Symbol";
            tRowTitle.Cells.Add(tCellNameTitle);

            // Date
            TableCell tCellDateTitle = new TableCell();
            tCellDateTitle.Text = "Date";
            tRowTitle.Cells.Add(tCellDateTitle);

            foreach (string col in d.Columns)
            {
                // Ajout d'une case (correspondant à une valeur)
                TableCell tCellVal = new TableCell();
                tCellVal.Text = col;
                tRowTitle.Cells.Add(tCellVal);
            }

            // Ajout des différentes lignes
            int n = d.Ds.Tables[0].Rows.Count;

            for (int i = 0; i < n; i++)
            {
                string name = (string)d.Ds.Tables[0].Rows[i]["Symbol"];
                DateTime time = (DateTime)d.Ds.Tables[0].Rows[i]["Date"];

                // On crée une nouvelle ligne
                TableRow tRow = new TableRow();
                Table1.Rows.Add(tRow);

                // Nom de l'actif
                TableCell tCellName = new TableCell();
                tCellName.Text = name;
                tRow.Cells.Add(tCellName);

                // Date
                TableCell tCellDate = new TableCell();
                tCellDate.Text = time.ToString("dd/MM/yyyy");
                tRow.Cells.Add(tCellDate);

                foreach (string s in d.Columns)
                {
                    // Ajout d'une case (correspondant à une valeur)
                    TableCell tCellVal = new TableCell();
                    tCellVal.Text = d.Ds.Tables[0].Rows[i][s].ToString();
                    tRow.Cells.Add(tCellVal);
                }
            }
            #endregion
        }

        protected void testExchangeRate()
        {
            #region Recuperation des Donnees

            List<Data.Currency> l = new List<Data.Currency>();
            //TextBox1.Text
            l.Add(Data.Currency.ALL);
            l.Add(Data.Currency.AMD);

            DateTime debut = new DateTime(2013, 11, 10);
            DateTime fin = new DateTime(2014, 6, 3);

            ServiceReference.ExchangeRateServiceClient client = new ServiceReference.ExchangeRateServiceClient();
            DataExchangeRate d = client.getExchangeRate(Data.Currency.ADF, l, debut, fin, Data.Frequency.Monthly);
            client.Close();
            
            //Label1.Text = d.Dict["CA.PA"].Dict[fin][0].ToString() + " "
            //            + d.Dict["BNP.PA"].Dict[fin][0].ToString();
            Label1.Text = d.Ds.Tables[0].Rows[0]["Symbol"].ToString();
            #endregion

            #region Affichage
            // On affiche la bar de titre
            // On crée une nouvelle ligne
            TableRow tRowTitle = new TableRow();
            Table1.Rows.Add(tRowTitle);

            // Nom de l'actif
            TableCell tCellNameTitle = new TableCell();
            tCellNameTitle.Text = "Symbol";
            tRowTitle.Cells.Add(tCellNameTitle);

            // Date
            TableCell tCellDateTitle = new TableCell();
            tCellDateTitle.Text = "Date";
            tRowTitle.Cells.Add(tCellDateTitle);

            foreach (string col in d.Columns)
            {
                // Ajout d'une case (correspondant à une valeur)
                TableCell tCellVal = new TableCell();
                tCellVal.Text = col;
                tRowTitle.Cells.Add(tCellVal);
            }

            // Ajout des différentes lignes
            int n = d.Ds.Tables[0].Rows.Count;

            for (int i = 0; i < n; i++)
            {
                string name = (string)d.Ds.Tables[0].Rows[i]["Symbol"];
                DateTime time = (DateTime)d.Ds.Tables[0].Rows[i]["Date"];

                // On crée une nouvelle ligne
                TableRow tRow = new TableRow();
                Table1.Rows.Add(tRow);

                // Nom de l'actif
                TableCell tCellName = new TableCell();
                tCellName.Text = name;
                tRow.Cells.Add(tCellName);

                // Date
                TableCell tCellDate = new TableCell();
                tCellDate.Text = time.ToString("dd/MM/yyyy");
                tRow.Cells.Add(tCellDate);

                foreach (string s in d.Columns)
                {
                    // Ajout d'une case (correspondant à une valeur)
                    TableCell tCellVal = new TableCell();
                    tCellVal.Text = d.Ds.Tables[0].Rows[i][s].ToString();
                    tRow.Cells.Add(tCellVal);
                }
            }
            #endregion
        }
    }
}