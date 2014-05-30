using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace AppliFiMag
{
    class CurrencyFormVM : ViewModel
    {
        private APIFiMag.Frequency _Frequence;

        public APIFiMag.Frequency Frequence
        {
            get { return _Frequence; }
            set
            {
                _Frequence = value;
                NotifyPropertyChanged("Frequence");
            }
        }

        public IEnumerable<APIFiMag.Frequency> FreqValues
        {
            get
            {
                return Enum.GetValues(typeof(APIFiMag.Frequency))
                    .Cast<APIFiMag.Frequency>();
            }
        }

        public IEnumerable<APIFiMag.Currency> CurrValues
        {
            get
            {
                return Enum.GetValues(typeof(APIFiMag.Currency))
                    .Cast<APIFiMag.Currency>();
            }
        }

        private APIFiMag.Currency _Symbol;

        public APIFiMag.Currency Symbol
        {
            get { return _Symbol; }
            set
            {
                _Symbol = value;
                NotifyPropertyChanged("Symbol");
            }
        }

        private DateTime _Debut;

        public DateTime Debut
        {
            get { return _Debut; }
            set
            {
                _Debut = value;
                NotifyPropertyChanged("Debut");
            }
        }

        private DateTime _Fin;

        public DateTime Fin
        {
            get { return _Fin; }
            set
            {
                _Fin = value;
                NotifyPropertyChanged("Fin");
            }
        }

        private CurrencyMakeCmd _Add;

        public CurrencyMakeCmd Add
        {
            get { return _Add; }
            set
            {
                _Add = value;
                NotifyPropertyChanged("Add");
            }
        }

        private ObservableCollection<APIFiMag.Currency> _Devises;

        public ObservableCollection<APIFiMag.Currency> Devises
        {
            get { return _Devises; }
            set
            {
                _Devises = value;
                NotifyPropertyChanged("Devises");
            }
        }

        private CmdAddCurr _AddCurr;

        public CmdAddCurr AddCurr
        {
            get { return _AddCurr; }
            set
            {
                _AddCurr = value;
                NotifyPropertyChanged("AddCurr");
            }
        }

        public CurrencyFormVM()
        {
            _Debut = DateTime.Now;
            _Fin = _Debut;
            _Symbol = CurrValues.First();
            _Frequence = FreqValues.First();
            _Devises = new ObservableCollection<APIFiMag.Currency>();
            _Add = new CurrencyMakeCmd(this);
            _AddCurr = new CmdAddCurr(this);
        }
    }
}
