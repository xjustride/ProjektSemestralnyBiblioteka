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
	/// Interaction logic for WyborUsuwania.xaml
	/// </summary>
	public partial class WyborUsuwania : Page
	{
		public WyborUsuwania()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Event handler for the "PrzegladajUsuwanieCzytelnikow" button click event.
		/// Opens the "PrzegladanieUsuwanieCzytelnikow" window and closes the current window.
		/// </summary>
		private void PrzegladajUsuwanieCzytelnikow_Click(object sender, RoutedEventArgs e)
		{
			PrzegladanieUsuwanieCzytelnikow pucz = new PrzegladanieUsuwanieCzytelnikow();
			pucz.Show();
			Window.GetWindow(this).Close();
		}

		/// <summary>
		/// Event handler for the "PrzegladajUsuwanieWypozyczen" button click event.
		/// Opens the "PrzegladanieDanych" window and closes the current window.
		/// </summary>
		private void PrzegladajUsuwanieWypozyczen_Click(object sender, RoutedEventArgs e)
		{
			PrzegladanieDanych pd = new PrzegladanieDanych();
			pd.Show();
			Window.GetWindow(this).Close();
		}

		/// <summary>
		/// Event handler for the "PrzegladajUsuwanieKsiazek" button click event.
		/// Opens the "PrzegladajKsiazki" window and closes the current window.
		/// </summary>
		private void PrzegladajUsuwanieKsiazek_Click(object sender, RoutedEventArgs e)
		{
			PrzegladajKsiazki pk = new PrzegladajKsiazki();
			pk.Show();
			Window.GetWindow(this).Close();
		}
	}
}
