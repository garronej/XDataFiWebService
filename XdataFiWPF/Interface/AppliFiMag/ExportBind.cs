using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace AppliFiMag
{
    class ExportBind : ViewModel, APIFiMag.Export
    {
        private APIFiMag.Data _Model;

        public APIFiMag.Data Model
        {
            get { return _Model; }
            set
            {
                _Model = value;
                NotifyPropertyChanged("Model");
            }
        }

        private Dictionary<DataKey, Dictionary<string, double>> _Data;

        public Dictionary<DataKey, Dictionary<string, double>> Data
        {
            get { return _Data; }
            set
            {
                _Data = value;
                NotifyPropertyChanged("Data");
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

        public ExportBind(APIFiMag.Data d)
        {
            _Model = d;
            Export(d);
        }

        public void Export(APIFiMag.Data d)
        {
            var temp = new Dictionary<DataKey, Dictionary<string, double>>(new DataKey.EqualityComparer());

            var actifs = (from x in d.Table.Data
                          group x by x.Symbol into y
                          select y);

            foreach (var symbol in actifs)
            {

                //écriture des différentes lignes de la table Historical dans le fichier
                var rows = (from x in symbol
                            group x by x.Date into y
                            select y);

                foreach (var date in rows)
                {
                    DataKey cle = new DataKey(date.Key, symbol.Key);
                    temp[cle] = new Dictionary<string, double>();
                    foreach (var item in d.Columns)
                    {
                        var val = (from x in date
                                   where x.Column == item
                                   select x.Value).First();
                        temp[cle][item] = val;
                    }
                }
            }

            Data = temp;
        }
    }

    class DataKey
    {

        public DateTime Date { get; private set; }

        public string Symbol { get; private set; }

        public DataKey(DateTime date, string symbol)
        {
            Date = date;
            Symbol = symbol;
        }

        public class EqualityComparer : IEqualityComparer<DataKey>
        {

            public bool Equals(DataKey x, DataKey y)
            {
                return x.Date == y.Date && x.Symbol == y.Symbol;
            }

            public int GetHashCode(DataKey obj)
            {
                return obj.Date.GetHashCode() ^ obj.Symbol.GetHashCode();
            }
        }

    }
}
