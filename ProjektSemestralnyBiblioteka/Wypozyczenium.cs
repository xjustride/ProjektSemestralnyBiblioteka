using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjektSemestralnyBiblioteka;

public partial class Wypozyczenium
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

    public int? KsiazkaId { get; set; }

    public int? CzytelnikId { get; set; }

    public DateTime? DataWypozyczenia { get; set; }

    public DateTime? DataZwrotu { get; set; }

    public virtual Czytelnicy? Czytelnik { get; set; }

    public virtual Ksiazki? Ksiazka { get; set; }
}
