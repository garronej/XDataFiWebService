using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfXDataFi.ServiceReference;

namespace WpfXDataFi
{
	/// <summary>
	/// Logique d'interaction pour Resultats.xaml
	/// </summary>
	public partial class Resultats : UserControl
	{
		private Data d;
		
		public Resultats(Data d)
		{
			this.InitializeComponent();
			this.d = d;
			
			display();
		}
		
		public void display()
		{
			// Colors
			SolidColorBrush titleBarColor = (SolidColorBrush)App.Current.FindResource("TitleBarColor");
			SolidColorBrush lightBarColor = (SolidColorBrush)App.Current.FindResource("LightBarColor");
			SolidColorBrush darkBarColor  = (SolidColorBrush)App.Current.FindResource("DarkBarColor");
			
			// Efface le contenu
			contenu.ColumnDefinitions.Clear();
            contenu.RowDefinitions.Clear();
            contenu.Children.Clear();

            contenu.HorizontalAlignment = HorizontalAlignment.Left;
            contenu.VerticalAlignment = VerticalAlignment.Top;
            contenu.Margin = new Thickness(0, 0, 0, 0);
            contenu.ShowGridLines = false;
            contenu.Background = new SolidColorBrush(Colors.LightGreen);


            ColumnDefinition c1 = new ColumnDefinition();
			c1.Width = new GridLength(80);
            contenu.ColumnDefinitions.Add(c1);
			
            ColumnDefinition c2 = new ColumnDefinition();
			c2.Width = new GridLength(100);
            contenu.ColumnDefinitions.Add(c2);

            RowDefinition r1 = new RowDefinition();
            r1.Height = new GridLength(60);
            contenu.RowDefinitions.Add(r1);
			
			MyGrid mg1 = new MyGrid(titleBarColor, "Symbole", "TextCenter");
            Grid.SetColumn(mg1, 0);
            Grid.SetRow(mg1, 0);
			
			MyGrid mg2 = new MyGrid(titleBarColor, "Date", "TextCenter");
            Grid.SetColumn(mg2, 1);
            Grid.SetRow(mg2, 0);

            contenu.Children.Add(mg1);
            contenu.Children.Add(mg2);

            //on créer les colonnes et on remplit les titres
            int j = 0;
            foreach (string col in d.Columns)
            {
                ColumnDefinition c = new ColumnDefinition();
				c.Width = new GridLength(60);
                contenu.ColumnDefinitions.Add(c);
				
				MyGrid mg = new MyGrid(titleBarColor, col, "TextCenter");
				Grid.SetColumn(mg, 2+j);
            	Grid.SetRow(mg, 0);
                contenu.Children.Add(mg);
                j++;
            }

            //on créer et on remplit les lignes
            int n = d.Ds.Tables[0].Rows.Count;

            for (int i = 0; i < n; i++)
            {
				// Couleur de fond
				SolidColorBrush color = darkBarColor;
                if (i % 2 == 0)
                {
                    color = lightBarColor;
                }
				
                string name = (string) d.Ds.Tables[0].Rows[i]["Symbol"];
                DateTime time = (DateTime) d.Ds.Tables[0].Rows[i]["Date"];
				
				// Ligne
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(60);
                contenu.RowDefinitions.Add(r);

				// Nom
                MyGrid mg3 = new MyGrid(color, name, "TextCenter");
                Grid.SetColumn(mg3, 0);
                Grid.SetRow(mg3, i + 1);
                contenu.Children.Add(mg3);
				
				// Date
				String date = time.ToString("dd/MM/yyyy");
				MyGrid mg4 = new MyGrid(color, date, "TextCenter");
                Grid.SetColumn(mg4, 1);
                Grid.SetRow(mg4, i + 1);
                contenu.Children.Add(mg4);

                int k = 0;
                foreach (string s in d.Columns)
                {
                    // Ajout d'une case (correspondant à une valeur)
                    string val = d.Ds.Tables[0].Rows[i][s].ToString();

					MyGrid mg = new MyGrid(color, val, "TextRight");
                    Grid.SetColumn(mg, 2+k);
                    Grid.SetRow(mg, i+1);
                    contenu.Children.Add(mg);
                    k++;
                }
            }
			
		}
	}
}