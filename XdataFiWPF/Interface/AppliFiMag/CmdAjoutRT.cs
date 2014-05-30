using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AppliFiMag
{
    public class CmdAjoutRT : ICommand
    {
        private MainVM _VM;
		public CmdAjoutRT(MainVM vm)
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
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MainVM.Instance.Content = new RealTimeForm();
        }
    }
}
