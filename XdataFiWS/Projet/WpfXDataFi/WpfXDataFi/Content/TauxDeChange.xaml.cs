using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfXDataFi.ServiceReference;

namespace WpfXDataFi
{
	/// <summary>
	/// Logique d'interaction pour TauxDeChange.xaml
	/// </summary>
	public partial class TauxDeChange : UserControl
	{
        MainWindow mw;

		public TauxDeChange(MainWindow mw)
		{
			this.InitializeComponent();
            this.mw = mw;
			
			foreach (Data.Currency cur in (Data.Currency[])Enum.GetValues(typeof(Data.Currency))) {
            
                Devise1.Items.Add(cur);
                Devise2.Items.Add(cur);
            } 

            DateDebut.SelectedDate = DateTime.Today;
            DateFin.SelectedDate = DateTime.Today;
		}
		
		 private void EnterTauxChange_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			// Récupération des devises
            Data.Currency currencyToCompare = (Data.Currency)
                Enum.Parse(typeof(Data.Currency), Devise1.SelectedValue.ToString());

			Data.Currency currency2 = (Data.Currency)
                Enum.Parse(typeof(Data.Currency), Devise2.SelectedValue.ToString());
			
            List<Data.Currency> l = new List<Data.Currency>();
            l.Add(currency2);

			// Récupération des dates
            DateTime debut = Convert.ToDateTime(DateDebut.SelectedDate.ToString());
            DateTime fin = Convert.ToDateTime(DateFin.SelectedDate.ToString());
			
			// Traitement des données
            try
            {
                ServiceReference.ExchangeRateServiceClient client = new ServiceReference.ExchangeRateServiceClient();
                DataExchangeRate d = client.getExchangeRate(currencyToCompare, l, debut, fin, Data.Frequency.Monthly);
                client.Close();
				
				mw.showRes(d);
            }
            catch (Exception ex)
            {
                //TO DO ouvrir une page d'erreur pour demander au client de recommencer la manoeuvre
                Console.WriteLine("Une erreur s'est produite");
            }
        }
	}
}