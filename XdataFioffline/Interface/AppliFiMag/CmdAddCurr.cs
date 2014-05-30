using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AppliFiMag
{
    class CmdAddCurr : ICommand
    {
		private CurrencyFormVM _VM;

		public CmdAddCurr(CurrencyFormVM vm)
        {
            _VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (parameter is APIFiMag.Currency)
				_VM.Devises.Add((APIFiMag.Currency)parameter);
        }
    }
}
