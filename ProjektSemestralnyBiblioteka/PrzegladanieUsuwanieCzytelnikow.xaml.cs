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

			// Initialize reader data
			czytelnicy = PobierzCzytelnikowZBazyDanych();
			lstDane.ItemsSource = czytelnicy;
		}

		/// <summary>
		/// Retrieves readers from the database.
		/// </summary>
		/// <returns>A list of readers.</returns>
		private List<Czytelnicy> PobierzCzytelnikowZBazyDanych()
		{
			using (var context = new ProjektSemContext())
			{
				return context.Czytelnicies.ToList();
			}
		}

		/// <summary>
		/// Event handler for the "Usun" button click event.
		/// Deletes the selected reader and their associated loans from the database.
		/// </summary>
		private void Usun_Click(object sender, RoutedEventArgs e)
		{
			// Check if a reader has been selected for deletion
			if (lstDane.SelectedItem == null)
			{
				MessageBox.Show("Please select a reader to delete.");
				return;
			}

			// Get the selected reader
			var wybranyCzytelnik = lstDane.SelectedItem as Czytelnicy;

			using (var context = new ProjektSemContext())
			{
				// Check if the reader exists in the database
				var czytelnik = context.Czytelnicies.Include(c => c.Wypozyczenia).FirstOrDefault(c => c.Id == wybranyCzytelnik.Id);

				if (czytelnik == null)
				{
					MessageBox.Show("The selected reader does not exist.");
					return;
				}

				// Remove associated loans from the database
				foreach (var wypozyczenie in czytelnik.Wypozyczenia.ToList())
				{
					context.Wypozyczenia.Remove(wypozyczenie);
				}

				// Remove the reader from the database
				context.Czytelnicies.Remove(czytelnik);
				context.SaveChanges();
			}

			// Refresh the data list view
			czytelnicy.Remove(wybranyCzytelnik);
			lstDane.ItemsSource = null;
			lstDane.ItemsSource = czytelnicy;

			MessageBox.Show("The reader has been deleted along with their associated loans.");
		}

		/// <summary>
		/// Event handler for the "Powrot" button click event.
		/// Closes the current window and returns to the previous window (MainWindow).
		/// </summary>
		private void Powrot_Click(object sender, RoutedEventArgs e)
		{
			// Close the data browsing window and return to the previous window (MainWindow)
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Close();
		}
	}
}
