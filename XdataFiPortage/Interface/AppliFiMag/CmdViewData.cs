using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AppliFiMag
{
    public class CmdViewData : ICommand
    {
		private MainVM _VM;

		public CmdViewData(MainVM vm)
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
			MainVM.Instance.Content = new ViewFiMag(_VM.Datas);
        }
    }
}
