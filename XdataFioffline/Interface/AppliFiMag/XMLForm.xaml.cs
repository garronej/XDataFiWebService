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
	/// Logique d'interaction pour XMLForm.xaml
	/// </summary>
	public partial class XMLForm : UserControl
	{
		public XMLForm()
		{
			InitializeComponent();
			this.DataContext = new XMLFormVM();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

			// Set filter for file extension and default file extension 
			dlg.DefaultExt = ".xml";
			dlg.Filter = "Extensible Markup Language (*.xml)|*.xml";


			// Display OpenFileDialog by calling ShowDialog method 
			Nullable<bool> result = dlg.ShowDialog();


			// Get the selected file name and display in a TextBox 
			if (result == true)
			{
				((XMLFormVM)this.DataContext).Symbol = dlg.FileName;
			}
		}
	}
}
