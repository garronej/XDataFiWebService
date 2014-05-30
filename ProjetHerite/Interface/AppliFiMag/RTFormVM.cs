using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace AppliFiMag
{
    class RTFormVM : ViewModel
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

        private RTMakeCmd _Add;

        public RTMakeCmd Add
        {
            get { return _Add; }
            set
            {
                _Add = value;
                NotifyPropertyChanged("Add");
            }
        }

        private int _Periode;

        public int Periode
        {
            get { return _Periode; }
            set
            {
                _Periode = value;
                NotifyPropertyChanged("Periode");
            }
        }

        private int _Duree;

        public int Duree
        {
            get { return _Duree; }
            set
            {
                _Duree = value;
                NotifyPropertyChanged("Duree");
            }
        }

        public RTFormVM()
        {
            _Add = new RTMakeCmd(this);
            _Periode = 0;
            _Duree = 0;
        }
    }
}
