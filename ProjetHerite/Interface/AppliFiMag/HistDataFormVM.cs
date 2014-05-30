using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace AppliFiMag
{
	class HistDataFormVM : ViewModel
	{
		private ObservableCollection<string> _Symbol;

		public ObservableCollection<string> Symbol
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

		private CmdMakeHistData _Add;

		public CmdMakeHistData Add
		{
			get { return _Add; }
			set
			{
				_Add = value;
				NotifyPropertyChanged("Add");
			}
		}

		private ObservableCollection<APIFiMag.Import> _Importers;

		public ObservableCollection<APIFiMag.Import> Importers
		{
			get { return _Importers; }
			set
			{
				_Importers = value;
				NotifyPropertyChanged("Importers");
			}
		}

		private APIFiMag.Import _SelectedImporter;

		public APIFiMag.Import SelectedImporter
		{
			get { return _SelectedImporter; }
			set
			{
				_SelectedImporter = value;
				NotifyPropertyChanged("SelectedImporter");
			}
		}

		private bool _High;

		public bool High
		{
			get { return _High; }
			set
			{
				_High = value;
				NotifyPropertyChanged("High");
			}
		}

		private bool _Low;

		public bool Low
		{
			get { return _Low; }
			set
			{
				_Low = value;
				NotifyPropertyChanged("Low");
			}
		}

		private bool _Open;

		public bool Open
		{
			get { return _Open; }
			set
			{
				_Open = value;
				NotifyPropertyChanged("Open");
			}
		}

		private bool _Close;

		public bool Close
		{
			get { return _Close; }
			set
			{
				_Close = value;
				NotifyPropertyChanged("Close");
			}
		}

		private bool _Volume;

		public bool Volume
		{
			get { return _Volume; }
			set
			{
				_Volume = value;
				NotifyPropertyChanged("Volume");
			}
		}
		private CmdAddSymbol _AddSymb;

		public CmdAddSymbol AddSymb
		{
			get { return _AddSymb; }
			set
			{
				_AddSymb = value;
				NotifyPropertyChanged("AddSymb");
			}
        }

        private string _Info;

        public string Info
        {
            get { return _Info; }
            set
            {
                _Info = value;
                NotifyPropertyChanged("Info");
            }
        }

		public HistDataFormVM()
		{
			_Symbol = new ObservableCollection<string>();
			_Debut = DateTime.Now;
			_Fin = _Debut;
			_Add = new CmdMakeHistData(this);
			_Importers = new ObservableCollection<APIFiMag.Import>();
			_Importers.Add(new APIFiMag.Importer.ImportYahoo());
			_Importers.Add(new APIFiMag.Importer.ImportGoogle());
			_SelectedImporter = _Importers.First();
			_High = true;
			_Low = true;
			_Open = true;
			_Close = true;
			_Volume = true;
			_AddSymb = new CmdAddSymbol(this);

		}
	}

	public class ConvertImportToString : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is APIFiMag.Importer.ImportGoogle)
			{
				return "Google";
			}
			else if (value is APIFiMag.Importer.ImportYahoo)
			{
				return "Yahoo";
			}
			return "WTF is that Importer ?!";
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
