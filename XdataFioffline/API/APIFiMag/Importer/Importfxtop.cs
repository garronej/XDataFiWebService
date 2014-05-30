using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Globalization;

namespace APIFiMag.Importer
{
    class Importfxtop : ParserCSV
    {
        public Importfxtop()
			: base("", CultureInfo.GetCultureInfo("EN"), true)
		{ 
			//TODO : test de connexion
		}

		public override void Import(Data d)
		{
			WebClient client = new WebClient();
			_Filepath = "Fxtop" + d.Symbol + "_" + d.Debut.ToString("dd-MM-yy") + "_" + d.Fin.ToString("dd-MM-yy") + ".csv";
			if (System.IO.File.Exists(@_Filepath))
			{
				//le fichier existe : on le supprime
				System.IO.File.Delete(@_Filepath);
			}
			Uri siteUri;
			switch (d.Type)
			{
				case TypeData.HistoricalData:
					// exemple d'url voulu : http://ichart.finance.yahoo.com/table.csv?s=GOOG&d=5&e=8&f=2013&g=d&a=5&b=8&c=2011&ignore=.csv

					int moiDebt = d.Debut.Month - 1;
					int moiFin = d.Fin.Month - 1;

					siteUri = new Uri("http://ichart.finance.yahoo.com/table.csv?s=" + d.Symbol + "&d=" + moiFin + "&e=" + d.Fin.Day + "&f=" + d.Fin.Year + "&g=d" + "&a=" + moiDebt + "&b=" + d.Debut.Day + "&c=" + d.Debut.Year + "&ignore=.csv");
                    client.DownloadFile(siteUri, _Filepath);
					break;
				default:
					//TODO : throw exception
					break;
			}
			base.Import(d);
		}
    }
}
