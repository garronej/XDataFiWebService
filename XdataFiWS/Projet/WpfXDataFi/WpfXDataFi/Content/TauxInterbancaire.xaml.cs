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
		}
		
		private void EnterInterBancaire_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO : ajoutez ici l'implémentation du gestionnaire d'événements.
        }
	}
}