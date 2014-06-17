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
	/// Logique d'interaction pour XML.xaml
	/// </summary>
	public partial class XML : UserControl
	{
		MainWindow mw;
		
		public XML(MainWindow mw)
		{
			this.InitializeComponent();
            this.mw = mw;
		}
		
		public void getFilepathXML()
		{
			// Create OpenFileDialog 
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
	
			// Set filter for file extension and default file extension 
			dlg.DefaultExt = ".xml";
			dlg.Filter = "Comma-separated values (*.xml)|*.xml";
	
	
			// Display OpenFileDialog by calling ShowDialog method 
			Nullable<bool> result = dlg.ShowDialog();
	
	
			// Get the selected file name and display in a TextBox 
			if (result == true)
			{
				string filename = dlg.FileName;
				
				StreamReader str = new StreamReader(filename);
				string content = str.ReadToEnd();
				str.Close();
				
				// Traitement des données
				try
				{
					ServiceReference.XMLServiceClient client = new ServiceReference.XMLServiceClient();
					Data d = client.getXML(content);
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
}