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

namespace WpfApplication
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        List<String> Item = new List<String>();
       
        public MainWindow()
        {
            Item.Add("EUR");
            Item.Add("USD");
            InitializeComponent();
            
			
            
            
			
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	
			int IdDevise2 = Devise2.SelectedIndex;
           
            String Date2 = DateFin.SelectedDate.ToString();

            

//XdataFiWebService.HelloWcfServiceClient client = new XdataFiWebService.HelloWcfServiceClient();

            //client.Currency Devise1 = client.Currency(Devise1.SelectedValue.ToString());
            //DateTime Date1 = Convert.ToDateTime(DateDebut.SelectedDate.ToString());
           
            //client.devise()
			
			
        }
    }
}
