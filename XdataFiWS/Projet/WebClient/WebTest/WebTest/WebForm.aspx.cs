using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
            columns.Add(Data.HistoricalColumn.High);

            DateTime debut = new DateTime(2013, 1, 1);
            DateTime fin = new DateTime(2013, 12,25);

            ServiceReference.ActifServiceClient client = new ServiceReference.ActifServiceClient();
            DataActif d = client.getActifHistorique(l, columns, debut , fin);
            client.Close();
        }
    }
}