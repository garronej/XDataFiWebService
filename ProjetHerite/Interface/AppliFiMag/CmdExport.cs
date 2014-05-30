using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.IO;
using System.Windows.Controls;

namespace AppliFiMag
{
	public class CmdExport : ICommand
	{
		MainVM _VM;

		public CmdExport(MainVM vm)
		{
			_VM = vm;
			_VM.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Instance_PropertyChanged);
		}

		void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			CanExecuteChanged(sender, e);
		}

		public bool CanExecute(object parameter)
		{
			return (_VM.Datas != null);
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			try
			{
				// Create OpenFileDialog 
				Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();

				// Set filter for file extension and default file extension 
				dlg.DefaultExt = ".csv";
				dlg.Filter = "Comma-separated values (*.csv)|*.csv"
					+ "|JavaScript Object Notation (*.json)|*.json"
					+ "|Extensible Markup Language (*.xml)|*.xml"
					+ "|SQL Server Database File (*.mdf)|*.mdf";


				// Display OpenFileDialog by calling ShowDialog method 
				Nullable<bool> result = dlg.ShowDialog();


				// Get the selected file name and display in a TextBox 
				if (result == true)
				{
					// Open document 
					string filename = dlg.FileName;
					switch (Path.GetExtension(filename))
					{
						case ".csv":
							_VM.Datas.Export(new APIFiMag.ExportCSV(filename));
							break;
						case ".json":
							_VM.Datas.Export(new APIFiMag.ExportJSON(filename));
							break;
						case ".xml":
							_VM.Datas.Export(new APIFiMag.ExportXML(filename));
							break;
						case ".mdf":
							var s = System.IO.Path.GetFileName(filename);
							_VM.Datas.Export(new APIFiMag.Exporter.ExportMDF(s));
							CpyFile(s, filename);
							break;
						default:
							break;
					}
				}
			}
			catch (Exception)
			{
				TextBlock t = new TextBlock();
				t.Text = "Erreur pendant l'exportation de données";
				t.TextAlignment = System.Windows.TextAlignment.Center;
				t.VerticalAlignment = System.Windows.VerticalAlignment.Center;
				MainVM.Instance.Datas = null;
				MainVM.Instance.Content = t;
			}
		}


		public void CpyFile(string source, string dest)
		{
			try
			{
				File.Copy(source, dest);
			}
			catch (Exception)
			{
				System.Threading.Thread.Sleep(500);
				CpyFile(source, dest);
			}
		}
	}
}
