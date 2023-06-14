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

namespace ProjektSemestralnyBiblioteka
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			//"Data Source=MSI;Initial Catalog=ProjektSem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
			InitializeComponent();
		}

		/// <summary>
		/// Event handler for the "DodajKsiazke" button click event.
		/// Opens the "GlownyInterfejs" window and closes the current window.
		/// </summary>
		private void DodajKsiazke_Click(object sender, RoutedEventArgs e)
		{
			GlownyInterfejs gi = new GlownyInterfejs();
			gi.Show();
			this.Close();
		}

		/// <summary>
		/// Event handler for the "DodajCzytelnika" button click event.
		/// Opens the "DodawanieCzytelnikow" window and closes the current window.
		/// </summary>
		private void DodajCzytelnika_Click(object sender, RoutedEventArgs e)
		{
			DodawanieCzytelnikow dcz = new DodawanieCzytelnikow();
			dcz.Show();
			this.Close();
		}

		/// <summary>
		/// Event handler for the "Wypozyczenia" button click event.
		/// Opens the "FormularzWypozyczania" window and closes the current window.
		/// </summary>
		private void Wypozyczenia_Click(object sender, RoutedEventArgs e)
		{
			FormularzWypozyczania fw = new FormularzWypozyczania();
			fw.Show();
			this.Close();
		}

		/// <summary>
		/// Event handler for the "PrzegladajUsuwajDane" button click event.
		/// Sets the content of the window to the "WyborUsuwania" user control.
		/// </summary>
		private void PrzegladajUsuwajDane_Click(object sender, RoutedEventArgs e)
		{
			WyborUsuwania wu = new WyborUsuwania();
			this.Content = wu;
		}
	}
}
