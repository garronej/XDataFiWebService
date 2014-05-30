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
using APIFiMag;

namespace AppliFiMag
{
	/// <summary>
	/// Logique d'interaction pour ViewFiMag.xaml
	/// </summary>
	public partial class ViewFiMag : UserControl
	{
		public ViewFiMag(Data d)
		{
			InitializeComponent();
			foreach (var item in d.Columns)
			{
				var x = new DataGridTextColumn();
				x.Header = item;
				x.Binding = new Binding("Value[" + item + "]");
				dg.Columns.Add(x);
			}
			var vm = new ExportBind(d);
			this.DataContext = vm;
		}
	}
}
