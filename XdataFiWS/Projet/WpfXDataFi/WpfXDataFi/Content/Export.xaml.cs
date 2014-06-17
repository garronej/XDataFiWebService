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

namespace WpfXDataFi
{
	/// <summary>
	/// Logique d'interaction pour Export.xaml
	/// </summary>
	public partial class Export : UserControl
	{
		public Export()
		{
			this.InitializeComponent();
		}
		
		public void getFilepathExport(Data d)
		{
			try
			{
				// Create OpenFileDialog 
				Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

				// Set filter for file extension and default file extension 
				dlg.DefaultExt = ".csv";
				dlg.Filter = "Comma-separated values (*.csv)|*.csv";


				// Display OpenFileDialog by calling ShowDialog method 
				Nullable<bool> result = dlg.ShowDialog();


				// Get the selected file name and display in a TextBox 
				if (result == true)
				{
					// Open document 
					string filename = dlg.FileName;
					StreamWriter sw = new StreamWriter(filename);
					string col = "Symbol";
					col += ";";
					col += "Date";
					foreach (string c in d.Columns){
						col += ";";
						col += c;
					}
					sw.WriteLine(col);
					
					int n = d.Ds.Tables[0].Rows.Count;
					for (int i = 0; i<n; i++){
						string row = (string)d.Ds.Tables[0].Rows[i]["Symbol"];
						row += ";";
						row += d.Ds.Tables[0].Rows[i]["Date"].ToString();
						
						foreach (string c in d.Columns){
							row += ";";
							row +=  d.Ds.Tables[0].Rows[i][c].ToString();
						}
						sw.WriteLine(row);
					}
					sw.Close();
					
					TextBlock t = new TextBlock();
					t.Text = "L'export a bien été effectué.";
					t.TextAlignment = System.Windows.TextAlignment.Center;
					t.VerticalAlignment = System.Windows.VerticalAlignment.Center;
				}
			}
			catch (Exception)
			{
				TextBlock t = new TextBlock();
				t.Text = "Erreur pendant l'exportation de données";
				t.TextAlignment = System.Windows.TextAlignment.Center;
				t.VerticalAlignment = System.Windows.VerticalAlignment.Center;
			}
		}
	}
}