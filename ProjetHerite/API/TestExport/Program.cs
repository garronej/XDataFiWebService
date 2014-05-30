using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APIFiMag;
using APIFiMag.Importer;
using APIFiMag.Exporter;
using APIFiMag.Datas;

namespace TestExport
{
	class Program
	{
		static void Main(string[] args)
		{
            //Data d = new Data("GOOG");
            //d.ImportData(new ImportRealTime(10000, 3, d));
            //d.Export(new ExportCSV("testtimer.csv"));
            //Console.WriteLine("Fini");
            //Console.ReadKey();
            var debut = new DateTime(1990, 6, 18);
            List<string> symb = new List<string>();
            symb.Add("GOOG");
            List<HistoricalColumn> lol = new List<HistoricalColumn>();
            lol.Add(HistoricalColumn.Close);
            lol.Add(HistoricalColumn.Open);

            DataActif d = new DataActif(symb, lol, debut, DateTime.Now);
            d.ImportData(new ImportGoogle());
            d.Export(new ExportMDF("evoli.mdf"));
		}
	}
}
