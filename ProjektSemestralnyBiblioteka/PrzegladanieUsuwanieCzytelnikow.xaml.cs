using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjektSemestralnyBiblioteka
{
	public partial class PrzegladanieUsuwanieCzytelnikow : Window
	{
		private List<Czytelnicy> czytelnicy;

		public PrzegladanieUsuwanieCzytelnikow()
		{
			InitializeComponent();
			// Inicjalizacja danych czytelników
			czytelnicy = PobierzCzytelnikowZBazyDanych();
			lstDane.ItemsSource = czytelnicy;
		}

		private List<Czytelnicy> PobierzCzytelnikowZBazyDanych()
		{
			using (var context = new ProjektSemContext())
			{
				return context.Czytelnicies.ToList();
			}
		}

		private void Usun_Click(object sender, RoutedEventArgs e)
		{// Sprawdź, czy został wybrany element do usunięcia
			if (lstDane.SelectedItem == null)
			{
				MessageBox.Show("Proszę wybrać czytelnika do usunięcia.");
				return;
			}

			// Pobierz wybranego czytelnika
			var wybranyCzytelnik = lstDane.SelectedItem as Czytelnicy;

			using (var context = new ProjektSemContext())
			{
				// Sprawdź, czy czytelnik istnieje w bazie danych
				var czytelnik = context.Czytelnicies.Include(c => c.Wypozyczenia).FirstOrDefault(c => c.Id == wybranyCzytelnik.Id);

				if (czytelnik == null)
				{
					MessageBox.Show("Wybrany czytelnik nie istnieje.");
					return;
				}

				// Usuń powiązane wypożyczenia z bazy danych
				foreach (var wypozyczenie in czytelnik.Wypozyczenia.ToList())
				{
					context.Wypozyczenia.Remove(wypozyczenie);
				}

				// Usuń czytelnika z bazy danych
				context.Czytelnicies.Remove(czytelnik);
				context.SaveChanges();
			}

			// Odśwież widok listy danych
			czytelnicy.Remove(wybranyCzytelnik);
			lstDane.ItemsSource = null;
			lstDane.ItemsSource = czytelnicy;

			MessageBox.Show("Czytelnik został usunięty wraz z powiązanymi wypożyczeniami."); ;
		}

		private void Powrot_Click(object sender, RoutedEventArgs e)
		{
			// Zamknij okno przeglądania danych i wróć do poprzedniego okna (MainWindow)
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Close();
		}
	}
}
