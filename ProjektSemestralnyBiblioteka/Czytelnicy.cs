using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjektSemestralnyBiblioteka;

public partial class Czytelnicy
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public string ImieNazwisko => $"{Imie} {Nazwisko}";
	public int Id { get; set; }

	public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string NumerTelefonu { get; set; } = null!;

	public virtual ICollection<Wypozyczenium> Wypozyczenia { get; set; } = new List<Wypozyczenium>();
}
