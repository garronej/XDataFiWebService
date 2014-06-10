using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Data;
using TauxDeChange.ServiceReference;

namespace TauxDeChange
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            foreach (Data.Currency cur in (Data.Currency[])Enum.GetValues(typeof(Data.Currency))) {
            
                Devise1.Items.Add(cur);
                Devise2.Items.Add(cur);
            } 

            DateDebut.SelectedDate = DateTime.Today;
            DateFin.SelectedDate = DateTime.Today;
        }

        private void EnterTauxChange_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Data.Currency currencyToCompare = (Data.Currency)
                Enum.Parse(typeof(Data.Currency), Devise1.SelectedValue.ToString());

            List<Data.Currency> l = new List<Data.Currency>();
            //TextBox1.Text
            l.Add((Data.Currency)
                Enum.Parse(typeof(Data.Currency), Devise2.SelectedValue.ToString()));

            DateTime debut = Convert.ToDateTime(DateDebut.SelectedDate.ToString());
            DateTime fin = Convert.ToDateTime(DateFin.SelectedDate.ToString());
            try
            {
                ServiceReference.ExchangeRateServiceClient client = new ServiceReference.ExchangeRateServiceClient();
                DataExchangeRate d = client.getExchangeRate(currencyToCompare, l, debut, fin, Data.Frequency.Monthly);
                client.Close();
            }
            catch (Exception ex)
            {
                //TO DO ouvrir une page d'erreur pour demander au client de recommencer la manoeuvre
                Console.WriteLine("Une erreur s'est produite");
            }
        }
    }
}
