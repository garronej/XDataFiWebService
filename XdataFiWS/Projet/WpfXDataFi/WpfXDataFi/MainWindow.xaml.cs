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
using WpfXDataFi.ServiceReference;

namespace WpfXDataFi
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		private Accueil A;
		private Historique H;
		private TauxInterbancaire TI;
		private TauxDeChange TC;
		private XML X;
		private Resultats R;
		private Export E;
		
        public MainWindow()
        {
            InitializeComponent();
			Menu.set(this);
			
			A = new Accueil();
			H = new Historique(this);
			TI = new TauxInterbancaire(this);
			TC = new TauxDeChange(this);
			X = new XML(this);
			E = new Export(this);
			
			showAccueil();
        }
		
		public void showAccueil()
		{
			Content.Content = A;
		}
		
		public void showHistorique()
		{
			Content.Content = H;
		}

        public void showInterestRate()
        {
            Content.Content = TI;
        }

        public void showChangeRate()
        {
            Content.Content = TC;
        }
		
		public void showChangeRate()
        {
            Content.Content = TC;
        }
		// Affiche la page de résultat précédente
		public void showRes()
		{
			Content.Content = R;
		}
		
		// Affiche une nouvelle page de résultat
		public void showRes(Data d)
		{
			R = new Resultats(d);
			Content.Content = R;
			Menu.showBarMenu();
		}
		
		public void showExport()
		{
			Content.Content = E;
			E.getFilepathExport(R.d);
		}
    }
}
