using System;
using System.Collections.Generic;

namespace ProjektSemestralnyBiblioteka;

public partial class Czytelnicy
{
    public int Id { get; set; }

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string NumerTelefonu { get; set; } = null!;

    public virtual ICollection<Wypozyczenium> Wypozyczenia { get; set; } = new List<Wypozyczenium>();
}
