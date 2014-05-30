using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using APIFiMag;
using APIFiMag.Importer;
using System.Windows.Controls;
using System.Threading;
using System.Xml;

namespace AppliFiMag
{
	class XMLMakeCmd : ICommand
	{
		delegate void TravailTermineDelegate();

		private XMLFormVM _VM;

		public XMLMakeCmd(XMLFormVM vm)
		{
			_VM = vm;
			vm.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(vm_PropertyChanged);
		}

		void vm_PropertyChanged(object sender, EventArgs e)
		{
			CanExecuteChanged(this, e);
		}

		public bool CanExecute(object parameter)
		{
			if (_VM.Symbol != null)
				return true;
			return false;
		}

		public event EventHandler CanExecuteChanged;
		private XmlDocument doc;

		public void Execute(object parameter)
		{
			MainVM.Instance.Content = new UserControl1();
			doc = new XmlDocument();
			doc.Load(_VM.Symbol);
			MainVM.Instance.Datas = new APIFiMag.Datas.DataXML(doc);
			Thread th = new Thread(threadMeth);
			th.Start();
		}

		void TravailTermine()
		{
			if (App.Current.Dispatcher.CheckAccess())
				MainVM.Instance.Content = new ViewFiMag(MainVM.Instance.Datas);
			else
				App.Current.Dispatcher.Invoke(new TravailTermineDelegate(TravailTermine));
		}

		void DuTravailEncoreDuTravail()
		{
			if (App.Current.Dispatcher.CheckAccess())
			{
				TextBlock t = new TextBlock();
				t.Text = "Erreur dans la récupération de données";
				t.TextAlignment = System.Windows.TextAlignment.Center;
				t.VerticalAlignment = System.Windows.VerticalAlignment.Center;
				MainVM.Instance.Datas = null;
				MainVM.Instance.Content = t;
			}
			else
				App.Current.Dispatcher.Invoke(new TravailTermineDelegate(DuTravailEncoreDuTravail));
		}

		public void threadMeth()
		{
			try
			{
				  //<RecupData>
				  //    <ListeIndex>
				  //        <Index>^FCHI</Index>
				  //    </ListeIndex>
				  //    <Composantes></Composantes>
				  //    <Champs>
				  //        <Param>Open</Param>
				  //        <Param>Volume</Param>
				  //    </Champs>
				  //    <DateDebut>2010-06-20</DateDebut>
				  //    <DateFin>2011-06-20</DateFin>
				  //    <Source>
				  //        <ChoixSource>yahoo</ChoixSource> 
				  //    </Source>
				  //    <ExportParam>
				  //        <Xml>exportTest.xml</Xml>
				  //    </ExportParam>
				  //</RecupData>

				XmlNode noeudSource = doc["RecupData"]["Source"];
				switch (noeudSource.FirstChild.InnerText)
				{
					case "yahoo":
						MainVM.Instance.Datas.ImportData(new ImportYahoo());
						break;
					case "google":
						MainVM.Instance.Datas.ImportData(new ImportGoogle());
						break;
					default:
						MainVM.Instance.Datas.ImportData(new ImportYahoo());
						break;
				}
				TravailTermine();
			}
			catch (Exception)
			{
				DuTravailEncoreDuTravail();
			}
		}
	}
}
