using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ProjektSemestralnyBiblioteka;

namespace ProjektSemestralnyBiblioteka
{
	public partial class PrzegladajKsiazki : Window
	{
		private List<Ksiazki> ksiazki;

		public PrzegladajKsiazki()
		{
			InitializeComponent();

			// Inicjalizacja danych książek
			ksiazki = PobierzKsiazkiZBazyDanych();
			lstDane.ItemsSource = ksiazki;
		}

		private List<Ksiazki> PobierzKsiazkiZBazyDanych()
		{
			using (var context = new ProjektSemContext())
			{
				return context.Ksiazkis.Include(k => k.Wypozyczenia).ToList();
			}
		}

		private void Usun_Click(object sender, RoutedEventArgs e)
		{
			// Sprawdź, czy została wybrana książka do usunięcia
			if (lstDane.SelectedItem == null)
			{
				MessageBox.Show("Proszę wybrać książkę do usunięcia.");
				return;
			}

			// Pobierz wybraną książkę
			var wybranaKsiazka = lstDane.SelectedItem as Ksiazki;

			using (var context = new ProjektSemContext())
			{
				// Sprawdź, czy książka istnieje w bazie danych
				var ksiazka = context.Ksiazkis.Include(k => k.Wypozyczenia).FirstOrDefault(k => k.Id == wybranaKsiazka.Id);

				if (ksiazka == null)
				{
					MessageBox.Show("Wybrana książka nie istnieje.");
					return;
				}

				// Usuń powiązane rekordy z tabeli "Wypozyczenia" dla danej książki
				foreach (var wypozyczenie in ksiazka.Wypozyczenia.ToList())
				{
					context.Wypozyczenia.Remove(wypozyczenie);
				}

				// Usuń książkę z bazy danych
				context.Ksiazkis.Remove(ksiazka);
				context.SaveChanges();
			}

			// Odśwież widok listy danych
			ksiazki.Remove(wybranaKsiazka);
			lstDane.ItemsSource = null;
			lstDane.ItemsSource = ksiazki;

			MessageBox.Show("Książka została usunięta.");
		}

		private void Powrot_Click(object sender, RoutedEventArgs e)
		{
			// Zamknij okno przeglądania książek i wróć do poprzedniego okna (MainWindow)
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Close();
		}
	}
}
