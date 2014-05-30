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
	class InterestMakeCmd : ICommand
	{
		delegate void TravailTermineDelegate();

		private InterestFormVM _VM;

		public InterestMakeCmd(InterestFormVM vm)
		{
			_VM = vm;
			vm.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(vm_PropertyChanged);
		}

		void vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			CanExecuteChanged(this, e);
		}

		public bool CanExecute(object parameter)
		{
			if (_VM.Debut <= _VM.Fin)
				return true;
			return false;
		}

		public event EventHandler CanExecuteChanged;

		public void Execute(object parameter)
		{
			MainVM.Instance.Content = new UserControl1();
			MainVM.Instance.Datas = new APIFiMag.Datas.DataIRate(_VM.Symbol, _VM.Debut, _VM.Fin);
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
				MainVM.Instance.Datas.ImportData(new ImportEBF());
				TravailTermine();
			}
			catch (Exception)
			{
				DuTravailEncoreDuTravail();
			}
		}
	}
}
