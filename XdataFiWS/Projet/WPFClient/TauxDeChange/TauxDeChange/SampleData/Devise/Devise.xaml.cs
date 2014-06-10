//      *********    NE PAS MODIFIER CE FICHIER     *********
//      Ce fichier est régénéré par un outil de création.Modifier
// .     ce fichier peut provoquer des erreurs.
namespace Expression.Blend.SampleData.Devise
{
	using System; 
	using System.ComponentModel;

// Pour réduire de façon significative le volume des données échantillons dans votre application de production, vous pouvez définir
// la constante de compilation conditionnelle DISABLE_SAMPLE_DATA et désactiver les données échantillons lors de l'exécution.
#if DISABLE_SAMPLE_DATA
	internal class Devise { }
#else

	public class Devise : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public Devise()
		{
			try
			{
				Uri resourceUri = new Uri("/TauxDeChange;component/SampleData/Devise/Devise.xaml", UriKind.RelativeOrAbsolute);
				System.Windows.Application.LoadComponent(this, resourceUri);
			}
			catch
			{
			}
		}

		private ItemCollection _Collection = new ItemCollection();

		public ItemCollection Collection
		{
			get
			{
				return this._Collection;
			}
		}
	}

	public class Item : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private string _DeviseList = string.Empty;

		public string DeviseList
		{
			get
			{
				return this._DeviseList;
			}

			set
			{
				if (this._DeviseList != value)
				{
					this._DeviseList = value;
					this.OnPropertyChanged("DeviseList");
				}
			}
		}
	}

	public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
	{ 
	}
#endif
}
