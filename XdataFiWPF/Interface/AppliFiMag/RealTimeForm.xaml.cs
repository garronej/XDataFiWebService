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
using System.Xml.Linq;

namespace AppliFiMag
{
	/// <summary>
	/// Logique d'interaction pour RealTimeForm.xaml
	/// </summary>
	public partial class RealTimeForm : UserControl
	{
		public RealTimeForm()
		{
			InitializeComponent();
			this.DataContext = new RTFormVM();
			this.treeView.DataContext = AssetList.Instance.Doc;
		}

		private void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			try
			{
				var tv = (TreeView)sender;
				var item = (XElement)tv.SelectedItem;
				string symb = item.Attribute("symbol").Value;
				((RTFormVM)this.DataContext).Symbol = symb;
			}
			catch (Exception)
			{

			}
		}
	}
}
