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
using System.Windows.Shapes;

namespace WpfXDataFi
{
	/// <summary>
	/// Classe contenant du texte qui est centré dans la case
	/// </summary>
	public class MyGrid : Grid
	{
		public MyGrid(SolidColorBrush color, String text, String nameStyle)
		{
			Background = color;
			HorizontalAlignment = HorizontalAlignment.Stretch;
			VerticalAlignment = VerticalAlignment.Stretch;
			
			TextBlock t = new TextBlock();
            t.Text = text;
			t.Style = (Style)App.Current.FindResource(nameStyle);
			
			Children.Add(t);
		}
	}
}