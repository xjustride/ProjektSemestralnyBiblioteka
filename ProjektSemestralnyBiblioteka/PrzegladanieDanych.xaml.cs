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

			// Initialize loan data
			wypozyczenia = PobierzWypozyczeniaZBazyDanych();
			lstDane.ItemsSource = wypozyczenia;
		}

		/// <summary>
		/// Retrieves loans from the database.
		/// </summary>
		/// <returns>A list of loans.</returns>
		private List<Wypozyczenium> PobierzWypozyczeniaZBazyDanych()
		{
			using (var context = new ProjektSemContext())
			{
				return context.Wypozyczenia.Include("Ksiazka").Include("Czytelnik").ToList();
			}
		}

		/// <summary>
		/// Event handler for the "Usun" button click event.
		/// Deletes the selected loan from the database.
		/// </summary>
		private void Usun_Click(object sender, RoutedEventArgs e)
		{
			// Check if an item has been selected for deletion
			if (lstDane.SelectedItem == null)
			{
				MessageBox.Show("Please select an item to delete.");
				return;
			}

			// Get the selected loan
			var wybraneWypozyczenie = lstDane.SelectedItem as Wypozyczenium;

			using (var context = new ProjektSemContext())
			{
				// Check if the loan exists in the database
				var wypozyczenie = context.Wypozyczenia.Find(wybraneWypozyczenie.Id);

				if (wypozyczenie == null)
				{
					MessageBox.Show("The selected loan does not exist.");
					return;
				}

				// Remove the loan from the database
				context.Wypozyczenia.Remove(wypozyczenie);
				context.SaveChanges();
			}

			// Refresh the data list view
			wypozyczenia.Remove(wybraneWypozyczenie);
			lstDane.ItemsSource = null;
			lstDane.ItemsSource = wypozyczenia;

			MessageBox.Show("The loan has been deleted.");
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
