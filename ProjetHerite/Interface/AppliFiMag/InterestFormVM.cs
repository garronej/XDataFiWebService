using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace AppliFiMag
{
    class InterestFormVM : ViewModel
    {
        public IEnumerable<APIFiMag.InterestRate> RateValues
        {
            get
            {
                return Enum.GetValues(typeof(APIFiMag.InterestRate))
                    .Cast<APIFiMag.InterestRate>();
            }
        }

        private APIFiMag.InterestRate _Symbol;

        public APIFiMag.InterestRate Symbol
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

        private InterestMakeCmd _Add;

        public InterestMakeCmd Add
        {
            get { return _Add; }
            set
            {
                _Add = value;
                NotifyPropertyChanged("Add");
            }
        }

        public InterestFormVM()
        {
            _Fin = DateTime.Now;
            _Debut = _Fin.AddDays(-1);
            _Symbol = RateValues.First();
            _Add = new InterestMakeCmd(this);
        }
    }
}
