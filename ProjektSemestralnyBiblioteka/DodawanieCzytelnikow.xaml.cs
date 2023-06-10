using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;

namespace ProjektSemestralnyBiblioteka
{
	/// <summary>
	/// Logika interakcji dla klasy DodawanieCzytelnikow.xaml
	/// </summary>
	public partial class DodawanieCzytelnikow : Window
	{
		private ProjektSemContext dbContext = new ProjektSemContext();

		public DodawanieCzytelnikow()
		{
			InitializeComponent();
		}

		private void DodajCzytelnika_Click(object sender, RoutedEventArgs e)
		{
			string imie = txtImie.Text;
			string nazwisko = txtNazwisko.Text;
			string numerTelefonu = txtNumerTelefonu.Text;

			if (string.IsNullOrEmpty(imie) || string.IsNullOrEmpty(nazwisko) || string.IsNullOrEmpty(numerTelefonu))
			{
				MessageBox.Show("Wprowadź wszystkie dane.");
				return;
			}

			try
			{
				Czytelnicy nowyCzytelnik = new Czytelnicy()
				{
					Id = dbContext.Czytelnicies.Max(c => c.Id) + 1,
					Imie = imie,
					Nazwisko = nazwisko,
					NumerTelefonu = numerTelefonu
				};

				dbContext.Czytelnicies.Add(nowyCzytelnik);
				dbContext.SaveChanges();

				MessageBox.Show("Dodano nowego czytelnika.");

				// Wyczyść pola tekstowe po dodaniu czytelnika
				txtImie.Text = string.Empty;
				txtNazwisko.Text = string.Empty;
				txtNumerTelefonu.Text = string.Empty;
			}
			catch (Exception ex)
			{
				//MessageBox.Show("Wystąpił błąd podczas dodawania czytelnika: " + ex.Message);
				MessageBox.Show("Wystąpił błąd podczas zapisywania zmian: " + ex.InnerException?.Message);
			}
		}

		private void Powrot_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mw = new MainWindow();
			mw.Show();
			this.Close();
		}
	}
}
