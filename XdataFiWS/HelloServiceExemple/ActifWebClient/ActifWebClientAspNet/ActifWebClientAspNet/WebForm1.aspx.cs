using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ActifWebClientAspNet
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ActifServiceReference.ActifServiceClient client = new ActifServiceReference.ActifServiceClient();
            Label1.Text = client.getActif(TextBox1.Text);
        }
    }
}