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
	/// Interaction logic for DodawanieCzytelnikow.xaml
	/// </summary>
	public partial class DodawanieCzytelnikow : Window
	{
		private ProjektSemContext dbContext = new ProjektSemContext();

		public DodawanieCzytelnikow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Event handler for the "DodajCzytelnika" button click event.
		/// Adds a new reader to the database based on the provided information.
		/// </summary>
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
				// Create a new instance of the "Czytelnicy" entity
				Czytelnicy nowyCzytelnik = new Czytelnicy()
				{
					// Assign the next available reader ID
					Id = dbContext.Czytelnicies.Max(c => c.Id) + 1,
					Imie = imie,
					Nazwisko = nazwisko,
					NumerTelefonu = numerTelefonu
				};

				// Add the new reader to the database context
				dbContext.Czytelnicies.Add(nowyCzytelnik);
				dbContext.SaveChanges();

				MessageBox.Show("Dodano nowego czytelnika.");

				// Clear the text fields after adding the reader
				txtImie.Text = string.Empty;
				txtNazwisko.Text = string.Empty;
				txtNumerTelefonu.Text = string.Empty;
			}
			catch (Exception ex)
			{
				// Show an error message if there is a problem with saving the changes
				MessageBox.Show("Wystąpił błąd podczas zapisywania zmian: " + ex.InnerException?.Message);
			}
		}

		/// <summary>
		/// Event handler for the "Powrot" button click event.
		/// Navigates back to the MainWindow and closes the current window.
		/// </summary>
		private void Powrot_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mw = new MainWindow();
			mw.Show();
			this.Close();
		}
	}
}
