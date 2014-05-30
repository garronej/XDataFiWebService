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
	/// Logique d'interaction pour DataForm.xaml
	/// </summary>
	public partial class DataForm : UserControl
	{
		public DataForm()
		{
			InitializeComponent();
			this.DataContext = new HistDataFormVM();
			XDocument doc = AssetList.Instance.Doc;
			this.treeView.DataContext = doc;
		}

		private void treeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			try
			{
				var tv = (TreeView)sender;
				var item = (XElement)tv.SelectedItem;
				string symb = item.Attribute("symbol").Value;
				if (!((HistDataFormVM)this.DataContext).Symbol.Contains(symb))
                    ((HistDataFormVM)this.DataContext).Symbol.Add(symb);
			}
			catch (Exception)
			{

			}
		}

		private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (sender is ListBox)
			{
				var lb = (ListBox)sender;
				string symb = (string)lb.SelectedItem;
				((HistDataFormVM)this.DataContext).Symbol.Remove(symb);
			}
		}

        private void treeView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var tv = (TreeView)sender;
                var item = (XElement)tv.SelectedItem;
                string symb = item.Attribute("symbol").Value;

                var info = new APIFiMag.InfosStatiques(symb);
                ((HistDataFormVM)this.DataContext).Info = APIFiMag.InfosStatiques.infosDispo.Name.ToString() + ": " + info.AccesInfos(APIFiMag.InfosStatiques.infosDispo.Name) + "\n"
                                                        + APIFiMag.InfosStatiques.infosDispo.Secteur.ToString() + ": " + info.AccesInfos(APIFiMag.InfosStatiques.infosDispo.Secteur) + "\n"
                                                        + APIFiMag.InfosStatiques.infosDispo.Industrie.ToString() + ": " + info.AccesInfos(APIFiMag.InfosStatiques.infosDispo.Industrie) + "\n"
                                                        + APIFiMag.InfosStatiques.infosDispo.NbEmployes.ToString() + ": " + info.AccesInfos(APIFiMag.InfosStatiques.infosDispo.NbEmployes) + "\n"
                                                        + APIFiMag.InfosStatiques.infosDispo.PlaceBoursiere.ToString() + ": " + info.AccesInfos(APIFiMag.InfosStatiques.infosDispo.PlaceBoursiere) + "\n"
                                                        + APIFiMag.InfosStatiques.infosDispo.Capitalisation.ToString() + ": " + info.AccesInfos(APIFiMag.InfosStatiques.infosDispo.Capitalisation) + "\n"
                                                        + APIFiMag.InfosStatiques.infosDispo.Isin.ToString() + ": " + info.AccesInfos(APIFiMag.InfosStatiques.infosDispo.Isin) + "\n";
            }
            catch (Exception)
            {

            }
        }
	}
}
