using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using APIFiMag;
using APIFiMag.Importer;
using System.Windows.Controls;
using System.Threading;

namespace AppliFiMag
{
	class CmdMakeHistData : ICommand
	{
		delegate void TravailTermineDelegate();

		private HistDataFormVM _VM;

		public CmdMakeHistData(HistDataFormVM vm)
		{
			_VM = vm;
			vm.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(vm_PropertyChanged);
			vm.Symbol.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(vm_PropertyChanged);
		}

		void vm_PropertyChanged(object sender, EventArgs e)
		{
			CanExecuteChanged(this, e);
		}

		public bool CanExecute(object parameter)
		{
			if (_VM.Symbol.Count > 0 && _VM.Debut < _VM.Fin && _VM.SelectedImporter != null)
				return true;
			return false;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
				MainVM.Instance.Content = new UserControl1();
				List<HistoricalColumn> colonnes = new List<HistoricalColumn>();
				if (_VM.High)
					colonnes.Add(HistoricalColumn.High);
				if (_VM.Low)
					colonnes.Add(HistoricalColumn.Low);
				if (_VM.Open)
					colonnes.Add(HistoricalColumn.Open);
				if (_VM.Close)
					colonnes.Add(HistoricalColumn.Close);
				if (_VM.Volume)
					colonnes.Add(HistoricalColumn.Volume);
				MainVM.Instance.Datas = new APIFiMag.Datas.DataActif(_VM.Symbol.ToList(), colonnes, _VM.Debut, _VM.Fin);
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
				MainVM.Instance.Datas.ImportData(_VM.SelectedImporter);
				TravailTermine();
			}
			catch (Exception)
			{
				DuTravailEncoreDuTravail();
			}
		}
	}
}
