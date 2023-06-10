using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjektSemestralnyBiblioteka;

public partial class ProjektSemContext : DbContext
{
    public ProjektSemContext()
    {
    }

    public ProjektSemContext(DbContextOptions<ProjektSemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Czytelnicy> Czytelnicies { get; set; }

    public virtual DbSet<Kategorie> Kategories { get; set; }

    public virtual DbSet<Ksiazki> Ksiazkis { get; set; }

    public virtual DbSet<Wypozyczenium> Wypozyczenia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=ProjektSem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Czytelnicy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Czytelni__3214EC27179839E9");

            entity.ToTable("Czytelnicy");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Imie)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nazwisko)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumerTelefonu)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Kategorie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Kategori__3214EC27EA411A51");

            entity.ToTable("Kategorie");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ksiazki>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ksiazki__3214EC27CCEE0D16");

            entity.ToTable("Ksiazki");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Autor)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Tytul)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Wypozyczenium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Wypozycz__3214EC27A8C8568D");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CzytelnikId).HasColumnName("CzytelnikID");
            entity.Property(e => e.DataWypozyczenia).HasColumnType("date");
            entity.Property(e => e.DataZwrotu).HasColumnType("date");
            entity.Property(e => e.KsiazkaId).HasColumnName("KsiazkaID");

            entity.HasOne(d => d.Czytelnik).WithMany(p => p.Wypozyczenia)
                .HasForeignKey(d => d.CzytelnikId)
                .HasConstraintName("FK__Wypozycze__Czyte__3C69FB99");

            entity.HasOne(d => d.Ksiazka).WithMany(p => p.Wypozyczenia)
                .HasForeignKey(d => d.KsiazkaId)
                .HasConstraintName("FK__Wypozycze__Ksiaz__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
