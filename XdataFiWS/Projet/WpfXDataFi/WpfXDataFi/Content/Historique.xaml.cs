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
	/// Logique d'interaction pour Historique.xaml
	/// </summary>
	public partial class Historique : UserControl
	{
        MainWindow mw;

		Boolean check1 = false;
        Boolean check2 = false;
        Boolean check3 = false;
        Boolean check4 = false;
        Boolean check5 = false;

        public Historique(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        private void EnterHistorique_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            List<String> l = new List<String>();
            l.Add(Actif.Text);
			
            List<Data.HistoricalColumn> columns = new List<Data.HistoricalColumn>();
            if (check1) { columns.Add((Data.HistoricalColumn)Enum.Parse(typeof(Data.HistoricalColumn), "Open")); }
            if (check2) { columns.Add((Data.HistoricalColumn)Enum.Parse(typeof(Data.HistoricalColumn), "Close")); }
            if (check3) { columns.Add((Data.HistoricalColumn)Enum.Parse(typeof(Data.HistoricalColumn), "High")); }
            if (check4) { columns.Add((Data.HistoricalColumn)Enum.Parse(typeof(Data.HistoricalColumn), "Low")); }
            if (check5) { columns.Add((Data.HistoricalColumn)Enum.Parse(typeof(Data.HistoricalColumn), "Volume")); }

            DateTime debut = Convert.ToDateTime(Début.SelectedDate.ToString());
            DateTime fin = Convert.ToDateTime(Fin.SelectedDate.ToString());

            ServiceReference.ActifServiceClient client = new ServiceReference.ActifServiceClient();
            DataActif d = client.getActifHistorique(l, columns, debut, fin);
            client.Close();

            mw.showRes(d);
        }

        private void open_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            check1 = true;
        }

        private void open_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            check1 = false;
        }

        private void close_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            check2 = true;
        }

        private void close_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            check2 = false;
        }

        private void high_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            check3 = true;
        }

        private void high_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            check3 = false;
        }

        private void low_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            check4 = true;
        }

        private void low_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            check4 = false;
        }

        private void volume_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            check5 = true;
        }

        private void volume_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            check5 = false;
        }
	}
}