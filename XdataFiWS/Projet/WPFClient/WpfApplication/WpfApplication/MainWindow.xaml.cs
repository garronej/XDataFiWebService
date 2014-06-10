using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using WpfApplication.ServiceReference;
using System.Data;
using Expression.Blend.SampleData.Devise;

namespace WpfApplication
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {

        Boolean check1 = false;
        Boolean check2 = false;
        Boolean check3 = false;
        Boolean check4 = false;
        Boolean check5 = false;

        public MainWindow()
        {
            
            InitializeComponent();
            foreach (Data.Currency cur in (Data.Currency[])Enum.GetValues(typeof(Data.Currency)))
            {

                Devise1.Items.Add(cur);
                Devise2.Items.Add(cur);
            } 
            DateDebut.SelectedDate = DateTime.Today;
            DateFin.SelectedDate = DateTime.Today;
            
           
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
            Data.Currency currencyToCompare = (Data.Currency)
                Enum.Parse(typeof(Data.Currency), Devise1.SelectedValue.ToString());

            List<Data.Currency> l = new List<Data.Currency>();
            //TextBox1.Text
            l.Add((Data.Currency)
                Enum.Parse(typeof(Data.Currency), Devise2.SelectedValue.ToString()));

            DateTime debut = Convert.ToDateTime(DateDebut.SelectedDate.ToString());
            DateTime fin = Convert.ToDateTime(DateFin.SelectedDate.ToString());

            ServiceReference.ExchangeRateServiceClient client = new ServiceReference.ExchangeRateServiceClient();
            DataExchangeRate d = client.getExchangeRate(currencyToCompare, l, debut, fin, Data.Frequency.Monthly);
            client.Close();
            /*
            table tab = new table();
            tab.t = new Table();
            TableRow tRowTitle = new TableRow();
            tab.t.Rows.Add(tRowTitle);*/
            //dataGrid.ItemsSource = d.Ds.Tables[0].DefaultView;
            /*
            DataGridTextColumn c1 = new DataGridTextColumn();
            c1.Header = "Symbol";
            c1.Binding = new Binding("Symbol");
            c1.Width = 25;
            dataGrid.Columns.Add(c1);

            List<object> rows = new List<object>();

            string[] s = new String[1];
            s[0] = "test";
            rows.Add(s);

            dataGrid.ItemsSource = rows;
            */
            //contenu = new Grid();
            contenu.ColumnDefinitions.Clear();
            contenu.RowDefinitions.Clear();
            contenu.Children.Clear();

            contenu.Width = 800;
            contenu.Height = 100;
            contenu.HorizontalAlignment = HorizontalAlignment.Left;
            contenu.VerticalAlignment = VerticalAlignment.Top;
            contenu.Margin = new Thickness(104, 120, 0, 0);
            contenu.ShowGridLines = false;
            contenu.Background = new SolidColorBrush(Colors.LightGreen);


            ColumnDefinition c1 = new ColumnDefinition();
            contenu.ColumnDefinitions.Add(c1);
            ColumnDefinition c2 = new ColumnDefinition();
            contenu.ColumnDefinitions.Add(c2);

            RowDefinition r1 = new RowDefinition();
            r1.Height = new GridLength(60);
            contenu.RowDefinitions.Add(r1);

            TextBlock t1 = new TextBlock();
            t1.Background = new SolidColorBrush(Colors.Green);
            t1.Text = "Symbole";
            t1.FontSize = 14;
            Grid.SetColumn(t1, 0);
            Grid.SetRow(t1, 0);
            TextBlock t2 = new TextBlock();
            t2.Background = new SolidColorBrush(Colors.Green);
            t2.Text = "Date";
            t2.FontSize = 14;
            Grid.SetColumn(t2, 1);
            Grid.SetRow(t2, 0);

            contenu.Children.Add(t1);
            contenu.Children.Add(t2);

            //on créer les colonnes et on remplit les titres
            int j = 0;
            foreach (string col in d.Columns)
            {
                ColumnDefinition c = new ColumnDefinition();
                contenu.ColumnDefinitions.Add(c);
                TextBlock t = new TextBlock();
                t.Background = new SolidColorBrush(Colors.Green);
                t.Text = col;
                t.FontSize = 14;
                Grid.SetColumn(t, 2 + j);
                Grid.SetRow(t, 0);
                contenu.Children.Add(t);
                j++;
            }

            //on créer et on remplit les lignes
            int n = d.Ds.Tables[0].Rows.Count;

            for (int i = 0; i < n; i++)
            {
                string name = (string)d.Ds.Tables[0].Rows[i]["Symbol"];
                DateTime time = (DateTime)d.Ds.Tables[0].Rows[i]["Date"];

                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(60);
                contenu.RowDefinitions.Add(r);

                TextBlock tt = new TextBlock();
                if (i % 2 == 0)
                {
                    tt.Background = new SolidColorBrush(Colors.LawnGreen);
                }
                tt.Text = name;
                tt.FontSize = 14;
                Grid.SetColumn(tt, 0);
                Grid.SetRow(tt, i + 1);
                contenu.Children.Add(tt);

                TextBlock tt2 = new TextBlock();
                if (i % 2 == 0)
                {
                    tt2.Background = new SolidColorBrush(Colors.LawnGreen);
                }
                tt2.Text = time.ToString("dd/MM/yyyy");
                tt2.FontSize = 14;
                Grid.SetColumn(tt2, 1);
                Grid.SetRow(tt2, i + 1);
                contenu.Children.Add(tt2);

                int k = 0;
                foreach (string s in d.Columns)
                {
                    // Ajout d'une case (correspondant à une valeur)
                    TextBlock tt3 = new TextBlock();
                    if (i % 2 == 0)
                    {
                        tt3.Background = new SolidColorBrush(Colors.LawnGreen);
                    }
                    tt3.Text = d.Ds.Tables[0].Rows[i][s].ToString();
                    tt3.FontSize = 14;
                    Grid.SetColumn(tt3, 2 + k);
                    Grid.SetRow(tt3, i + 1);
                    contenu.Children.Add(tt3);
                    k++;
                }

            }
            /*
            ScrollViewer scroll = new ScrollViewer();
            scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            contenu.Children.Add(scroll);
            */
        }


        private void open_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            check1 = true;
        }

        private void open_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            check1 = false;
        }

        private void close_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            check2 = true;
        }

        private void close_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            check2 = false;
        }

        private void high_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            check3 = true;
        }

        private void high_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            check3 = false;
        }

        private void low_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            check4 = true;
        }

        private void low_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            check4 = false;
        }

        private void volume_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            check5 = true;
        }

        private void volume_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            check5 = false;
        }

        private void EnterHistorique_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	 List<String> l = new List<String>();
            l.Add(Actif.Text);
                
            List<Data.HistoricalColumn> columns = new List<Data.HistoricalColumn>();
            if (check1) { columns.Add((Data.HistoricalColumn) Enum.Parse(typeof(Data.HistoricalColumn), "Open")); }
            if (check2) { columns.Add((Data.HistoricalColumn) Enum.Parse(typeof(Data.HistoricalColumn), "Close")); }
            if (check3) { columns.Add((Data.HistoricalColumn) Enum.Parse(typeof(Data.HistoricalColumn), "High")); }
            if (check4) { columns.Add((Data.HistoricalColumn) Enum.Parse(typeof(Data.HistoricalColumn), "Low")); }
            if (check5) { columns.Add((Data.HistoricalColumn) Enum.Parse(typeof(Data.HistoricalColumn), "Volume")); }

            DateTime debut = Convert.ToDateTime(Début.SelectedDate.ToString());
            DateTime fin = Convert.ToDateTime(Fin.SelectedDate.ToString());

            ServiceReference.ActifServiceClient client = new ServiceReference.ActifServiceClient();
            DataActif d = client.getActifHistorique(l, columns, debut, fin);
            client.Close();


            contenu.ColumnDefinitions.Clear();
            contenu.RowDefinitions.Clear();
            contenu.Children.Clear();

            contenu.Width = 800;
            contenu.Height = 100;
            contenu.HorizontalAlignment = HorizontalAlignment.Left;
            contenu.VerticalAlignment = VerticalAlignment.Top;
            contenu.Margin = new Thickness(104, 120, 0, 0);
            contenu.ShowGridLines = false;
            contenu.Background = new SolidColorBrush(Colors.LightGreen);


            ColumnDefinition c1 = new ColumnDefinition();
            contenu.ColumnDefinitions.Add(c1);
            ColumnDefinition c2 = new ColumnDefinition();
            contenu.ColumnDefinitions.Add(c2);

            RowDefinition r1 = new RowDefinition();
            r1.Height = new GridLength(60);
            contenu.RowDefinitions.Add(r1);

            TextBlock t1 = new TextBlock();
            t1.Background = new SolidColorBrush(Colors.Green);
            t1.Text = "Symbole";
            t1.FontSize = 14;
            Grid.SetColumn(t1, 0);
            Grid.SetRow(t1, 0);
            TextBlock t2 = new TextBlock();
            t2.Background = new SolidColorBrush(Colors.Green);
            t2.Text = "Date";
            t2.FontSize = 14;
            Grid.SetColumn(t2, 1);
            Grid.SetRow(t2, 0);

            contenu.Children.Add(t1);
            contenu.Children.Add(t2);

            //on créer les colonnes et on remplit les titres
            int j = 0;
            foreach (string col in d.Columns)
            {
                ColumnDefinition c = new ColumnDefinition();
                contenu.ColumnDefinitions.Add(c);
                TextBlock t = new TextBlock();
                t.Background = new SolidColorBrush(Colors.Green);
                t.Text = col;
                t.FontSize = 14;
                Grid.SetColumn(t, 2 + j);
                Grid.SetRow(t, 0);
                contenu.Children.Add(t);
                j++;
            }

            //on créer et on remplit les lignes
            int n = d.Ds.Tables[0].Rows.Count;

            for (int i = 0; i < n; i++)
            {
                string name = (string)d.Ds.Tables[0].Rows[i]["Symbol"];
                DateTime time = (DateTime)d.Ds.Tables[0].Rows[i]["Date"];

                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(60);
                contenu.RowDefinitions.Add(r);

                TextBlock tt = new TextBlock();
                if (i % 2 == 0)
                {
                    tt.Background = new SolidColorBrush(Colors.LawnGreen);
                }
                tt.Text = name;
                tt.FontSize = 14;
                Grid.SetColumn(tt, 0);
                Grid.SetRow(tt, i + 1);
                contenu.Children.Add(tt);

                TextBlock tt2 = new TextBlock();
                if (i % 2 == 0)
                {
                    tt2.Background = new SolidColorBrush(Colors.LawnGreen);
                }
                tt2.Text = time.ToString("dd/MM/yyyy");
                tt2.FontSize = 14;
                Grid.SetColumn(tt2, 1);
                Grid.SetRow(tt2, i + 1);
                contenu.Children.Add(tt2);

                int k = 0;
                foreach (string s in d.Columns)
                {
                    // Ajout d'une case (correspondant à une valeur)
                    TextBlock tt3 = new TextBlock();
                    if (i % 2 == 0)
                    {
                        tt3.Background = new SolidColorBrush(Colors.LawnGreen);
                    }
                    tt3.Text = d.Ds.Tables[0].Rows[i][s].ToString();
                    tt3.FontSize = 14;
                    Grid.SetColumn(tt3, 2 + k);
                    Grid.SetRow(tt3, i + 1);
                    contenu.Children.Add(tt3);
                    k++;
                }

            }
        }

        private void DateDebut_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        	 DateFin.BlackoutDates.Add(new CalendarDateRange(new DateTime(1900, 1, 1),  Convert.ToDateTime(DateDebut.SelectedDate.ToString()).AddDays(-1)));
            
        }

        private void DateFin_CalendarOpened(object sender, System.Windows.RoutedEventArgs e)
        {
        	 DateFin.BlackoutDates.Add(new CalendarDateRange(new DateTime(2014, 1, 1),  Convert.ToDateTime(DateDebut.SelectedDate.ToString()).AddDays(-1)));
            
        }

        

    }
   
}
