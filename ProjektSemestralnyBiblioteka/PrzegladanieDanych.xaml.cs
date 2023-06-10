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
	/// Logika interakcji dla klasy PrzegladanieDanych.xaml
	/// </summary>
	public partial class PrzegladanieDanych : Window
	{
		public PrzegladanieDanych()
		{
			InitializeComponent();
		}

		private void Usun_Click(object sender, RoutedEventArgs e)
		{

        }

		private void Powrot_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mw = new MainWindow();
			mw.Show();
			this.Close();

		}
	}
}
