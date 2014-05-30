using System;
using System.Collections.Generic;
using APIFiMag;
using APIFiMag.Importer;
using APIFiMag.Datas;

namespace TestImport
{
    class Program
    {
        static void Main(string[] args)
        {

			//string[] col = { "actif1", "actif2", "actif3" };
			//var debut = new DateTime(1990, 6, 18);
			//var fin = new DateTime(2013, 6, 18);
			//var frequence = new TimeSpan(365, 0, 0, 0);
			//Data dataaaa = new Data(col.ToList(), debut, fin, frequence);
			//dataaaa.ImportData(new ParserCSV("testCSV.csv", 3, ';'));
			//dataaaa.Export(new ExportCSV("exportdelimport.csv", Langue.FR));

            //List<HistoricalColumn> colonnes = new List<HistoricalColumn>();
            //colonnes.Add(HistoricalColumn.Close);
            //colonnes.Add(HistoricalColumn.Open);
            //List<string> symbol = new List<string>();
            //symbol.Add("GOOG");
            //Data goog = new Data(symbol, colonnes, new DateTime(2012, 6, 9), DateTime.Now);
            //goog.ImportData(new ImportGoogle());
            //goog.Export(new ExportCSV("testGoogle.csv"));
            //Data yahoo = new Data(symbol, colonnes, new DateTime(2012, 6, 9), DateTime.Now);
            //yahoo.ImportData(new ImportYahoo());
            //yahoo.Export(new ExportCSV("testYahoo.csv"));
			Data ebf = new DataIRate(InterestRate.EUREPO, new DateTime(2012, 6, 9), DateTime.Now);
			ebf.ImportData(new ImportEBF());
			ebf.Export(new APIFiMag.Exporter.ExportMDF("testEBF.mdf"));
			//List<Currency> curr = new List<Currency>();
			//curr.Add(Currency.USD);
			//curr.Add(Currency.JPY);
			//curr.Add(Currency.LBP);
			//Frequency freq = Frequency.Yearly;
			//DataFXTop xchange = new DataFXTop(Currency.EUR, curr, new DateTime(2012, 6, 9), DateTime.Now, freq);
			//xchange.ImportData(new ParserFXTop());
			//xchange.Export(new ExportCSV("testFxTop.csv"));
            
        }
    }
}
    
