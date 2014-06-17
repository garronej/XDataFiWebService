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

namespace WpfXDataFi
{
	/// <summary>
	/// Logique d'interaction pour LeftMenuControl.xaml
	/// </summary>
	public partial class LeftMenuControl : UserControl
	{
		MainWindow mw;
		Button focusButton;
		
		public LeftMenuControl()
		{
			this.InitializeComponent();
			mw = null;
			focusButton = null;
		}
		
		public void set(MainWindow mw)
		{
			this.mw = mw;
		}
		
		public void showBarMenu()
		{
			ButtonData.Visibility = Visibility.Visible;
			ButtonMenu.Visibility = Visibility.Visible;
			
			changeFocus(ButtonData);
		}
		
		public void hideBarMenu()
		{
			ButtonData.Visibility = Visibility.Hidden;
			ButtonMenu.Visibility = Visibility.Hidden;
		}
		
		private void showAccueil(object sender, RoutedEventArgs e)
		{
            removeFocus();
			hideBarMenu();
			mw.showAccueil();
		}
		
		private void showHist(object sender, RoutedEventArgs e)
		{
			Button b = (Button) sender;
			changeFocus(b);
			mw.showHistorique();
		}

        private void showInterestRate(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            changeFocus(b);
            mw.showInterestRate();
        }

        private void showChangeRate(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            changeFocus(b);
            mw.showChangeRate();
		}
        
		
		private void showXML(object sender, RoutedEventArgs e)
        {
			Button b = (Button)sender;
            changeFocus(b);
            mw.showXML();
        }
		
		private void showRes(object sender, RoutedEventArgs e) 
		{
			Button b = (Button)sender;
            changeFocus(b);
            mw.showRes();
		}
		
		private void showExport(object sender, RoutedEventArgs e) 
		{
			Button b = (Button)sender;
            changeFocus(b);
            mw.showExport();
		}
		
		private void changeFocus(Button b)
		{
            removeFocus();
			focusButton = b;
			focusButton.Style = (Style)App.Current.FindResource("MenuButtonClick");
		}

        private void removeFocus()
        {
            if (focusButton != null)
            {
                focusButton.Style = (Style)App.Current.FindResource("MenuButton");
            }
        }
	}
}