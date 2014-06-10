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
using Taux_interbancaire.ServiceReference;

namespace Taux_interbancaire
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            DateDébut.SelectedDate = DateTime.Today;
            DateFin.SelectedDate = DateTime.Today;
        }

        private void EnterInterBancaire_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Data.InterestRate symbol = (Data.InterestRate)
                Enum.Parse(typeof(Data.InterestRate), TauxInterBancaire.SelectedItem.ToString());


            DateTime debut = Convert.ToDateTime(DateDébut.SelectedDate.ToString());
            DateTime fin = Convert.ToDateTime(DateFin.SelectedDate.ToString());

            ServiceReference.InterestRateServiceClient client = new ServiceReference.InterestRateServiceClient();
             DataInterestRate d = client.getInterestRate(symbol,debut,fin);
            client.Close();
        }
    }
}
