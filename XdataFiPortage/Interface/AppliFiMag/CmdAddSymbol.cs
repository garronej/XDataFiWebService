using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AppliFiMag
{
    class CmdAddSymbol : ICommand
    {
        private HistDataFormVM _VM;

        public CmdAddSymbol(HistDataFormVM vm)
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
            if (parameter is string
                && !String.IsNullOrWhiteSpace((string)parameter))
                _VM.Symbol.Add((string)parameter);
        }
    }
}
