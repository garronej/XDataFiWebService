using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using APIFiMag;
using APIFiMag.Importer;
using System.Windows.Data;
using System.Windows;

namespace AppliFiMag
{
    public class MainVM : ViewModel
    {
        private static MainVM _instance = new MainVM();

        public static MainVM Instance
        {
            get { return MainVM._instance; }
            set { MainVM._instance = value; }
        }

        private object _Content;

        public object Content
        {
            get { return _Content; }
            set
            {
                _Content = value;
                NotifyPropertyChanged("Content");
            }
        }

        private string _Nom;

        public string Nom
        {
            get { return _Nom; }
            set
            {
                _Nom = value;
                NotifyPropertyChanged("Nom");
            }
        }

        private APIFiMag.Data _Data;

        public APIFiMag.Data Datas
        {
            get { return _Data; }
            set
            {
                _Data = value;
                NotifyPropertyChanged("Datas");
            }
		}

		private CmdAjoutData _AddData;

		public CmdAjoutData AddData
		{
			get { return _AddData; }
			set
			{
				_AddData = value;
				NotifyPropertyChanged("AddData");
			}
		}

		private CmdAjoutRate _AddRate;

		public CmdAjoutRate AddRate
		{
			get { return _AddRate; }
			set
			{
				_AddRate = value;
				NotifyPropertyChanged("AddRate");
			}
		}

		private CmdAjoutCurr _AddCurr;

		public CmdAjoutCurr AddCurr
		{
			get { return _AddCurr; }
			set
			{
				_AddCurr = value;
				NotifyPropertyChanged("AddCurr");
			}
		}

		private CmdAjoutRT _AddRT;

		public CmdAjoutRT AddRT
		{
			get { return _AddRT; }
			set
			{
				_AddRT = value;
				NotifyPropertyChanged("AddRT");
			}
		}

		private CmdAjoutXML _AddXML;

		public CmdAjoutXML AddXML
		{
			get { return _AddXML; }
			set
			{
				_AddXML = value;
				NotifyPropertyChanged("AddXML");
			}
		}

        private CmdViewData _ViewData;

        public CmdViewData ViewData
        {
            get { return _ViewData; }
            set
            {
                _ViewData = value;
                NotifyPropertyChanged("ViewData");
            }
		}

		private CmdExport _ExpCmd;

		public CmdExport ExpCmd
		{
			get { return _ExpCmd; }
			set
			{
				_ExpCmd = value;
				NotifyPropertyChanged("ExpCmd");
			}
		}


        private MainVM()
        {
            _Nom = "API-FiMag";
			//Content = new DataForm();
			_AddData = new CmdAjoutData(this);
			_AddRate = new CmdAjoutRate();
            _ViewData = new CmdViewData(this);
			_AddCurr = new CmdAjoutCurr();
			_ExpCmd = new CmdExport(this);
			_AddRT = new CmdAjoutRT(this);
			_AddXML = new CmdAjoutXML();
        }
    }

	public class DataToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if ((APIFiMag.Data)value != null)
				return Visibility.Visible;
			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
