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
	/// Logique d'interaction pour SymbolTree.xaml
	/// </summary>
	public partial class SymbolTree : UserControl
	{
		public SymbolTree()
		{
			InitializeComponent();
			XDocument doc = AssetList.Instance.Doc;
			this.treeView.DataContext = doc;
		}
	}
}
