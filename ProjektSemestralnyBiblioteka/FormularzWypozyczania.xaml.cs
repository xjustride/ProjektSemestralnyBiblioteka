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
	/// Logika interakcji dla klasy FormularzWypozyczania.xaml
	/// </summary>
	public partial class FormularzWypozyczania : Window
	{
		private List<Ksiazki> ksiazki;
		private List<Czytelnicy> czytelnicy;

		public FormularzWypozyczania()
		{
			InitializeComponent();

			// Inicjalizacja danych książek
			ksiazki = PobierzKsiazkiZBazyDanych();
			cmbKsiazka.ItemsSource = ksiazki;
			cmbKsiazka.DisplayMemberPath = "Tytul";

			// Inicjalizacja danych czytelników
			czytelnicy = PobierzCzytelnikowZBazyDanych();
			cmbCzytelnik.ItemsSource = czytelnicy;
			cmbCzytelnik.DisplayMemberPath = "ImieNazwisko";
		}

		private List<Ksiazki> PobierzKsiazkiZBazyDanych()
		{
			using (var context = new ProjektSemContext ())
			{
				return context.Ksiazkis.ToList();
			}
		}

		private List<Czytelnicy> PobierzCzytelnikowZBazyDanych()
		{
			using (var context = new ProjektSemContext ())
			{
				return context.Czytelnicies.ToList();
			}
		}

		private void Wypozycz_Click(object sender, RoutedEventArgs e)
		{
			// Pobierz wybrane wartości z ComboBox-ów
			var wybranaKsiazka = cmbKsiazka.SelectedItem as Ksiazki;
			var wybranyCzytelnik = cmbCzytelnik.SelectedItem as Czytelnicy;
			var dataWypozyczenia = dpDataWypozyczenia.SelectedDate;
			var dataZwrotu = dpDataZwrotu.SelectedDate;

			if (wybranaKsiazka == null || wybranyCzytelnik == null || dataWypozyczenia == null || dataZwrotu == null)
			{
				MessageBox.Show("Proszę uzupełnić wszystkie pola formularza.");
				return;
			}

			using (var context = new ProjektSemContext())
			{
				// Sprawdź, czy książka i czytelnik istnieją w bazie danych
				var ksiazka = context.Ksiazkis.Find(wybranaKsiazka.Id);
				var czytelnik = context.Czytelnicies.Find(wybranyCzytelnik.Id);

				if (ksiazka == null || czytelnik == null)
				{
					MessageBox.Show("Wybrana książka lub czytelnik nie istnieje.");
					return;
				}

				// Tworzenie nowego wypożyczenia
				var wypozyczenie = new Wypozyczenium
				{
					Ksiazka = ksiazka,
					Czytelnik = czytelnik,
					DataWypozyczenia = dataWypozyczenia.Value,
					DataZwrotu = dataZwrotu.Value
				};

				context.Wypozyczenia.Add(wypozyczenie);
				context.SaveChanges();
			}

			MessageBox.Show("Książka została wypożyczona.");
		}

		private void Powrot_Click(object sender, RoutedEventArgs e)
		{
			// Zamknij okno formularza wypożyczania i wróć do poprzedniego okna (MainWindow)
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Close();
		}
	}
}
