using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using WebTest.ServiceReference;

namespace WebTest
{
    public partial class WebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<String> l = new List<string>();
            l.Add(TextBox1.Text);

            List<Data.HistoricalColumn> columns = new List<Data.HistoricalColumn>();
            columns.Add(Data.HistoricalColumn.Close);

            DateTime debut = new DateTime(2013, 1, 1);
            DateTime fin = new DateTime(2013, 12,25);

            ServiceReference.ActifServiceClient client = new ServiceReference.ActifServiceClient();
            DataActif d = client.getActifHistorique(l, columns, debut , fin);
            client.Close();

            //display_table(d);

            display_chart(d);

        }


        protected void display_chart(DataActif d)
        {
            Chart1 = new Chart();
            Chart1.Width = 800;
            Chart1.Height = 600;

            ChartArea area = new ChartArea("MainArea");
            area.AxisX.Title = "Date";
            area.AxisY.Title = "Valeur";

            area.AxisX.IntervalType = DateTimeIntervalType.Months;
            Chart1.ChartAreas.Add(area);

            Legend legend = new Legend();
            legend.Name = "Default";
            legend.Alignment = System.Drawing.StringAlignment.Center;
            Chart1.Legends.Add(legend);

            int n = d.Ds.Tables[0].Rows.Count;
            Series series = new Series(TextBox1.Text);

            for (int i = 0; i < n; i++)
            {
                series.Points.AddXY((DateTime)d.Ds.Tables[0].Rows[i]["Date"], (double)d.Ds.Tables[0].Rows[i]["Close"]);
            }

            Chart1.Series.Add(series);

            this.form1.Controls.Add(Chart1);


        }

        /*protected void display_table(DataActif d)
        {
            TableRow TRowTitle = new TableRow();
            Table1.Rows.Add(TRowTitle);

            TableCell TCellTitleName = new TableCell();
            TCellTitleName.Text = "Symbol";
            TRowTitle.Cells.Add(TCellTitleName);

            TableCell TCellTitleDate = new TableCell();
            TCellTitleDate.Text = "Date";
            TRowTitle.Cells.Add(TCellTitleDate);

            foreach (string col in d.Columns)
            {
                TableCell TCellVal = new TableCell();
                TCellVal.Text = col;
                TRowTitle.Cells.Add(TCellVal);
            }

            int n = d.Ds.Tables[0].Rows.Count;

            for (int i = 0; i < n; i++)
            {
                string name = (string)d.Ds.Tables[0].Rows[i]["Symbol"];
                DateTime date = (DateTime)d.Ds.Tables[0].Rows[i]["Date"];

                TableRow TRow = new TableRow();
                Table1.Rows.Add(TRow);

                TableCell TCellName = new TableCell();
                TCellName.Text = name;
                TRow.Cells.Add(TCellName);

                TableCell TCellDate = new TableCell();
                TCellDate.Text = date.ToString("dd/MM/yyyy");
                TRow.Cells.Add(TCellDate);

                foreach (string s in d.Columns)
                {
                    TableCell TCellVal = new TableCell();
                    TCellVal.Text = d.Ds.Tables[0].Rows[i][s].ToString();
                    TRow.Cells.Add(TCellVal);
                }

            }
        }*/
    }
}
