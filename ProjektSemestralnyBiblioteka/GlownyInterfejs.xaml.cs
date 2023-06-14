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
	/// Interaction logic for GlownyInterfejs.xaml
	/// </summary>
	public partial class GlownyInterfejs : Window
	{
		private ProjektSemContext dbContext = new ProjektSemContext();

		public GlownyInterfejs()
		{
			InitializeComponent();
			LoadCategories();
		}

		/// <summary>
		/// Loads the categories from the database and populates the ComboBox.
		/// </summary>
		private void LoadCategories()
		{
			var categories = dbContext.Kategories.ToList();
			cmbKategoria.ItemsSource = categories;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			using (var context = new ProjektSemContext())
			{
				List<Kategorie> categories = context.Kategories.ToList();
				cmbKategoria.ItemsSource = categories;
			}
		}

		/// <summary>
		/// Event handler for the "DodajKsiazke" button click event.
		/// Adds a new book to the database based on the provided information.
		/// </summary>
		private void DodajKsiazke_Click(object sender, RoutedEventArgs e)
		{
			string title = txtTytul.Text;
			string author = txtAutor.Text;
			int releaseYear;

			if (!int.TryParse(txtRokWydania.Text, out releaseYear))
			{
				MessageBox.Show("Invalid release year entered.");
				return;
			}

			Kategorie selectedCategory = cmbKategoria.SelectedItem as Kategorie;

			if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || selectedCategory == null)
			{
				MessageBox.Show("Please enter all the data.");
				return;
			}

			try
			{
				int maxId = dbContext.Ksiazkis.Any() ? dbContext.Ksiazkis.Max(c => c.Id) : 0;

				Ksiazki newBook = new Ksiazki()
				{
					Id = maxId + 1,
					Tytul = title,
					Autor = author,
					RokWydania = releaseYear,
				};

				dbContext.Ksiazkis.Add(newBook);
				dbContext.SaveChanges();

				MessageBox.Show("New book added.");

				// Clear the text fields after adding the book
				txtTytul.Text = string.Empty;
				txtAutor.Text = string.Empty;
				txtRokWydania.Text = string.Empty;
				cmbKategoria.SelectedIndex = -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occurred while adding the book: " + ex.Message);
			}
		}

		/// <summary>
		/// Event handler for the "Powrot" button click event.
		/// Closes the current window and returns to the main window (MainWindow).
		/// </summary>
		private void Powrot_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mw = new MainWindow();
			mw.Show();
			this.Close();
		}
	}
}
