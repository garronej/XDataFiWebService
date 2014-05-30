using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace AppliFiMag
{
    class XMLFormVM : ViewModel
    {

        private string _Symbol;

		public string Symbol
        {
            get { return _Symbol; }
            set
            {
                _Symbol = value;
                NotifyPropertyChanged("Symbol");
            }
        }

		private XMLMakeCmd _Add;

		public XMLMakeCmd Add
        {
            get { return _Add; }
            set
            {
                _Add = value;
                NotifyPropertyChanged("Add");
            }
        }

		public XMLFormVM()
        {
            _Add = new XMLMakeCmd(this);
        }
    }
}
