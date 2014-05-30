using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APIFiMag;

namespace TestExport
{
    class ExempleGetData : Import
    {
        public void Import(Data d)
        {
            //for (DateTime t = d.Debut; t <= d.Fin; t = t.Add(d.))
            //{
            //    var col = d.Columns;
            //    foreach (var item in col)
            //    {
            //        int c = (from x in d.Table.Data
            //                 where x.Symbol == item && x.Date == t
            //                 select x).Count();
            //        if (c == 0)
            //            d.Table.Data.AddDataRow(t,d.Symbol.First(), item, 42);
            //        Console.WriteLine("Ajout à la date " + t.ToString() + " à la colonne " + item);
            //    }
            //}
        }
    }
}
