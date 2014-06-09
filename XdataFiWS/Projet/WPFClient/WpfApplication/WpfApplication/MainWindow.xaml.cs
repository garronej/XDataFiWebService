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
        
      
        public MainWindow()
        {
            
            InitializeComponent();
            Devise1.Items.Add("USD");
            Devise1.Items.Add("EUR");
            Devise1.Items.Add("ADF");

            Devise2.Items.Add("USD");
            Devise2.Items.Add("EUR");
            Devise2.Items.Add("ADF");

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            /*
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
            client.Close();*/
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

            contenu.Width = 400;
            contenu.Height = 200;
            contenu.HorizontalAlignment = HorizontalAlignment.Left;
            contenu.VerticalAlignment = VerticalAlignment.Top;
            contenu.Margin = new Thickness(0, 0, 0, 0);
            contenu.ShowGridLines = true;
            contenu.Background = new SolidColorBrush(Colors.Aqua);
            
            ColumnDefinition c1 = new ColumnDefinition();
            contenu.ColumnDefinitions.Add(c1);

            RowDefinition r1 = new RowDefinition();
            r1.Height = new GridLength(60);
            contenu.RowDefinitions.Add(r1);

            TextBlock t = new TextBlock();
            t.Text = "OK";
            t.FontSize = 14;
            Grid.SetColumn(t, 0);
            Grid.SetRow(t, 0);

            contenu.Children.Add(t);

        }

    }
    public class table
    {

        public Table t;
    }
}
