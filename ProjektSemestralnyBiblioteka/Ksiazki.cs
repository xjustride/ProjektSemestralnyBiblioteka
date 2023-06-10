using System;
using System.Collections.Generic;

namespace ProjektSemestralnyBiblioteka;

public partial class Ksiazki
{
    public int Id { get; set; }

    public string Tytul { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public int RokWydania { get; set; }

    public virtual ICollection<Wypozyczenium> Wypozyczenia { get; set; } = new List<Wypozyczenium>();
}
