using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjektSemestralnyBiblioteka
{
	public partial class PrzegladanieDanych : Window
	{
		private List<Wypozyczenium> wypozyczenia;

		public PrzegladanieDanych()
		{
			InitializeComponent();

			// Inicjalizacja danych wypożyczeń
			wypozyczenia = PobierzWypozyczeniaZBazyDanych();
			lstDane.ItemsSource = wypozyczenia;
		}

		private List<Wypozyczenium> PobierzWypozyczeniaZBazyDanych()
		{
			using (var context = new ProjektSemContext())
			{
				return context.Wypozyczenia.Include("Ksiazka").Include("Czytelnik").ToList();
			}
		}

		private void Usun_Click(object sender, RoutedEventArgs e)
		{
			// Sprawdź, czy został wybrany element do usunięcia
			if (lstDane.SelectedItem == null)
			{
				MessageBox.Show("Proszę wybrać element do usunięcia.");
				return;
			}

			// Pobierz wybrane wypożyczenie
			var wybraneWypozyczenie = lstDane.SelectedItem as Wypozyczenium;

			using (var context = new ProjektSemContext())
			{
				// Sprawdź, czy wypożyczenie istnieje w bazie danych
				var wypozyczenie = context.Wypozyczenia.Find(wybraneWypozyczenie.Id);

				if (wypozyczenie == null)
				{
					MessageBox.Show("Wybrane wypożyczenie nie istnieje.");
					return;
				}

				// Usuń wypożyczenie z bazy danych
				context.Wypozyczenia.Remove(wypozyczenie);
				context.SaveChanges();
			}

			// Odśwież widok listy danych
			wypozyczenia.Remove(wybraneWypozyczenie);
			lstDane.ItemsSource = null;
			lstDane.ItemsSource = wypozyczenia;

			MessageBox.Show("Wypożyczenie zostało usunięte.");
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
