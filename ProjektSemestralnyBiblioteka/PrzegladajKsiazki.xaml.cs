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

			// Initialize book data
			ksiazki = PobierzKsiazkiZBazyDanych();
			lstDane.ItemsSource = ksiazki;
		}

		/// <summary>
		/// Retrieves books from the database.
		/// </summary>
		/// <returns>A list of books.</returns>
		private List<Ksiazki> PobierzKsiazkiZBazyDanych()
		{
			using (var context = new ProjektSemContext())
			{
				return context.Ksiazkis.Include(k => k.Wypozyczenia).ToList();
			}
		}

		/// <summary>
		/// Event handler for the "Usun" button click event.
		/// Deletes the selected book from the database.
		/// </summary>
		private void Usun_Click(object sender, RoutedEventArgs e)
		{
			// Check if a book has been selected for deletion
			if (lstDane.SelectedItem == null)
			{
				MessageBox.Show("Please select a book to delete.");
				return;
			}

			// Get the selected book
			var wybranaKsiazka = lstDane.SelectedItem as Ksiazki;

			using (var context = new ProjektSemContext())
			{
				// Check if the book exists in the database
				var ksiazka = context.Ksiazkis.Include(k => k.Wypozyczenia).FirstOrDefault(k => k.Id == wybranaKsiazka.Id);

				if (ksiazka == null)
				{
					MessageBox.Show("The selected book does not exist.");
					return;
				}

				// Remove associated records from the "Wypozyczenia" table for the selected book
				foreach (var wypozyczenie in ksiazka.Wypozyczenia.ToList())
				{
					context.Wypozyczenia.Remove(wypozyczenie);
				}

				// Remove the book from the database
				context.Ksiazkis.Remove(ksiazka);
				context.SaveChanges();
			}

			// Refresh the data list view
			ksiazki.Remove(wybranaKsiazka);
			lstDane.ItemsSource = null;
			lstDane.ItemsSource = ksiazki;

			MessageBox.Show("The book has been deleted.");
		}

		/// <summary>
		/// Event handler for the "Powrot" button click event.
		/// Closes the current window and returns to the previous window (MainWindow).
		/// </summary>
		private void Powrot_Click(object sender, RoutedEventArgs e)
		{
			// Close the book browsing window and return to the previous window (MainWindow)
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Close();
		}
	}
}
