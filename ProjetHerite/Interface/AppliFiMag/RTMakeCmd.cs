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
	class RTMakeCmd : ICommand
	{
		delegate void TravailTermineDelegate();

		private RTFormVM _VM;

		public RTMakeCmd(RTFormVM vm)
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

		public void Execute(object parameter)
		{
            MainVM.Instance.Content = new UserControl1();
            TimeSpan duree = new TimeSpan(0, 0, _VM.Duree);
            TimeSpan periode = new TimeSpan(0, 0, _VM.Periode);
			MainVM.Instance.Datas = new APIFiMag.Datas.DataRealTime(_VM.Symbol,duree,periode);
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
				MainVM.Instance.Datas.ImportData(new ImportReal());
				TravailTermine();
			}
			catch (Exception)
			{
				DuTravailEncoreDuTravail();
			}
		}
	}
}
