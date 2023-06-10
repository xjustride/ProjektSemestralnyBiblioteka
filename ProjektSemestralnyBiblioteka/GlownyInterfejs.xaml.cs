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
	/// Logika interakcji dla klasy GlownyInterfejs.xaml
	/// </summary>
	public partial class GlownyInterfejs : Window
	{
		private ProjektSemContext dbContext = new ProjektSemContext();

		public GlownyInterfejs()
		{
			InitializeComponent();
			LoadKategorie();
		}

		private void LoadKategorie()
		{
			var kategorie = dbContext.Kategories.ToList();
			cmbKategoria.ItemsSource = kategorie;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			using (var context = new ProjektSemContext())
			{
				List<Kategorie> kategorie = context.Kategories.ToList();
				cmbKategoria.ItemsSource = kategorie;
			}
		}
		private void DodajKsiazke_Click(object sender, RoutedEventArgs e)
		{
			string tytul = txtTytul.Text;
			string autor = txtAutor.Text;
			int rokWydania;

			if (!int.TryParse(txtRokWydania.Text, out rokWydania))
			{
				MessageBox.Show("Podano nieprawidłowy rok wydania.");
				return;
			}

			Kategorie wybranaKategoria = cmbKategoria.SelectedItem as Kategorie;

			if (string.IsNullOrEmpty(tytul) || string.IsNullOrEmpty(autor) || wybranaKategoria == null)
			{
				MessageBox.Show("Wprowadź wszystkie dane.");
				return;
			}

			try
			{
				Ksiazki nowaKsiazka = new Ksiazki()
				{
					Id = dbContext.Ksiazkis.Max(c => c.Id) + 1,
					Tytul = tytul,
					Autor = autor,
					RokWydania = rokWydania,
				};

				dbContext.Ksiazkis.Add(nowaKsiazka);
				dbContext.SaveChanges();

				MessageBox.Show("Dodano nową książkę.");

				// Wyczyść pola tekstowe po dodaniu książki
				txtTytul.Text = string.Empty;
				txtAutor.Text = string.Empty;
				txtRokWydania.Text = string.Empty;
				cmbKategoria.SelectedIndex = -1;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Wystąpił błąd podczas dodawania książki: " + ex.Message);
			}
		}

		private void Powrot_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mw = new MainWindow();
			mw.Show();
			this.Close();
		}
	}
}
