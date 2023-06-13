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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektSemestralnyBiblioteka
{
	/// <summary>
	/// Logika interakcji dla klasy WyborUsuwania.xaml
	/// </summary>
	public partial class WyborUsuwania : Page
	{
		public WyborUsuwania()
		{
			InitializeComponent();
		}

		private void PrzegladajUsuwanieCzytelnikow_Click(object sender, RoutedEventArgs e)
		{
			PrzegladanieUsuwanieCzytelnikow pucz = new PrzegladanieUsuwanieCzytelnikow();
			pucz.Show();
			Window.GetWindow(this).Close();
		}

		private void PrzegladajUsuwanieWypozyczen_Click(object sender, RoutedEventArgs e)
		{
			PrzegladanieDanych pd = new PrzegladanieDanych();
			pd.Show();
			Window.GetWindow(this).Close();
		}

		private void PrzegladajUsuwanieKsiazek_Click(object sender, RoutedEventArgs e)
		{
			PrzegladajKsiazki pk = new PrzegladajKsiazki();
			pk.Show();	
			Window.GetWindow(this).Close();
		}
	}
}
