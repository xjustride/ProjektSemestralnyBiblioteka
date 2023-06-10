using System;
using System.Collections.Generic;

namespace ProjektSemestralnyBiblioteka;

public partial class Wypozyczenium
{
    public int Id { get; set; }

    public int? KsiazkaId { get; set; }

    public int? CzytelnikId { get; set; }

    public DateTime? DataWypozyczenia { get; set; }

    public DateTime? DataZwrotu { get; set; }

    public virtual Czytelnicy? Czytelnik { get; set; }

    public virtual Ksiazki? Ksiazka { get; set; }
}
