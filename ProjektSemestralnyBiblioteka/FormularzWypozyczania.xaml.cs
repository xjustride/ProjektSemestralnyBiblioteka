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
	/// Interaction logic for FormularzWypozyczania.xaml
	/// </summary>
	public partial class FormularzWypozyczania : Window
	{
		private List<Ksiazki> ksiazki;
		private List<Czytelnicy> czytelnicy;

		public FormularzWypozyczania()
		{
			InitializeComponent();

			// Initialize book data
			ksiazki = GetBooksFromDatabase();
			cmbKsiazka.ItemsSource = ksiazki;
			cmbKsiazka.DisplayMemberPath = "Tytul";

			// Initialize reader data
			czytelnicy = GetReadersFromDatabase();
			cmbCzytelnik.ItemsSource = czytelnicy;
			cmbCzytelnik.DisplayMemberPath = "ImieNazwisko";
		}

		/// <summary>
		/// Retrieves book data from the database.
		/// </summary>
		/// <returns>List of books</returns>
		private List<Ksiazki> GetBooksFromDatabase()
		{
			using (var context = new ProjektSemContext())
			{
				return context.Ksiazkis.ToList();
			}
		}

		/// <summary>
		/// Retrieves reader data from the database.
		/// </summary>
		/// <returns>List of readers</returns>
		private List<Czytelnicy> GetReadersFromDatabase()
		{
			using (var context = new ProjektSemContext())
			{
				return context.Czytelnicies.ToList();
			}
		}

		/// <summary>
		/// Event handler for the "Wypozycz" button click event.
		/// Adds a new rental record to the database based on the selected information.
		/// </summary>
		private void Wypozycz_Click(object sender, RoutedEventArgs e)
		{
			var selectedBook = cmbKsiazka.SelectedItem as Ksiazki;
			var selectedReader = cmbCzytelnik.SelectedItem as Czytelnicy;
			var rentalDate = dpDataWypozyczenia.SelectedDate;
			var returnDate = dpDataZwrotu.SelectedDate;

			if (selectedBook == null || selectedReader == null || rentalDate == null || returnDate == null)
			{
				MessageBox.Show("Please fill in all the form fields.");
				return;
			}

			using (var context = new ProjektSemContext())
			{
				var book = context.Ksiazkis.Find(selectedBook.Id);
				var reader = context.Czytelnicies.Find(selectedReader.Id);

				if (book == null || reader == null)
				{
					MessageBox.Show("Selected book or reader does not exist.");
					return;
				}

				var maxId = context.Wypozyczenia.Any() ? context.Wypozyczenia.Max(c => c.Id) : 0;
				var rental = new Wypozyczenium
				{
					Id = maxId + 1,
					Ksiazka = book,
					Czytelnik = reader,
					DataWypozyczenia = rentalDate.Value,
					DataZwrotu = returnDate.Value
				};

				context.Wypozyczenia.Add(rental);
				context.SaveChanges();
			}

			MessageBox.Show("The book has been rented.");
		}

		/// <summary>
		/// Event handler for the "Powrot" button click event.
		/// Closes the current window and returns to the previous window (MainWindow).
		/// </summary>
		private void Powrot_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Close();
		}
	}
}
