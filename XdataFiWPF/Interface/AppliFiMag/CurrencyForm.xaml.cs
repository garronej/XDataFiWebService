using System;
using System.Collections.Generic;
using System.Linq;
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

namespace AppliFiMag
{
	/// <summary>
	/// Logique d'interaction pour CurrencyForm.xaml
	/// </summary>
	public partial class CurrencyForm : UserControl
	{
		public CurrencyForm()
		{
			InitializeComponent();
			this.DataContext = new CurrencyFormVM();
		}

		private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//if ((APIFiMag.Currency)((ListBox)sender).SelectedItem != null)
				((CurrencyFormVM)this.DataContext).Devises.Remove((APIFiMag.Currency)((ListBox)sender).SelectedItem);
		}
	}
}
