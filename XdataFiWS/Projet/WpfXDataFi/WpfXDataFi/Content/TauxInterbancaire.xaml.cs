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
	/// Logique d'interaction pour TauxInterbancaire.xaml
	/// </summary>
	public partial class TauxInterbancaire : UserControl
	{
        MainWindow mw;

		public TauxInterbancaire(MainWindow mw)
		{
			this.InitializeComponent();
            this.mw = mw;
			
			foreach (Data.InterestRate i in (Data.InterestRate[])Enum.GetValues(typeof(Data.InterestRate)))
            {

                TauxInterBancaire.Items.Add(i);
            } 

            DateDébut.SelectedDate = DateTime.Today;
            DateFin.SelectedDate = DateTime.Today;
		}
		
		private void EnterInterBancaire_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TauxInterBancaire.SelectedValue == null)
            {
                error.Text = "Vous devez selectionner un taux.";
                return;
            }
			// Taux de référence
        	Data.InterestRate symbol = (Data.InterestRate)
                Enum.Parse(typeof(Data.InterestRate), TauxInterBancaire.SelectedItem.ToString());

			// Dates
            DateTime debut = Convert.ToDateTime(DateDébut.SelectedDate.ToString());
            DateTime fin = Convert.ToDateTime(DateFin.SelectedDate.ToString());

          

            if (DateTime.Compare(debut, fin) >= 0)
            {
                error.Text = "Les dates choisies ne sont pas valides.";
                return;
            }
			// Traitement des données
            try
            {
				ServiceReference.InterestRateServiceClient client = new ServiceReference.InterestRateServiceClient();
				DataInterestRate d = client.getInterestRate(symbol, debut, fin);
				client.Close();
					
				mw.showRes(d);
            }
            catch (Exception ex)
            {
                error.Text = ex.Message;
                return;
            }
        }
	}
}