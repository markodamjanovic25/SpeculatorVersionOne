using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SpeculatorVersionOne.Models
{
    public partial class SpeculatorContext : IdentityDbContext<Korisnik, Uloga, string>
    {       

        public SpeculatorContext(DbContextOptions<SpeculatorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Korisnik> Korisnici { get; set; }
        public virtual DbSet<Kupovina> Kupovine { get; set; }
        public virtual DbSet<Prihod> Prihodi { get; set; }
        public virtual DbSet<Priliv> Prilivi { get; set; }
        public virtual DbSet<Proizvod> Proizvodi { get; set; }
        public virtual DbSet<Trosak> Troskovi { get; set; }
        public virtual DbSet<ZeljeniProizvod> ZeljeniProizvodi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Speculator;Trusted_Connection=true");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Korisnik>(entity =>
            {
                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Lozinka)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StanjeNaRacunu).HasColumnType("money");
            });

            modelBuilder.Entity<Kupovina>(entity =>
            {
                entity.HasKey(e => new { e.TrosakId, e.KorisnikId });

                entity.HasIndex(e => e.KorisnikId);

                entity.Property(e => e.TrosakId).HasColumnName("TrosakID");

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Kupovine)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kupovina_Korisnik");

                entity.HasOne(d => d.Trosak)
                    .WithMany(p => p.Kupovine)
                    .HasForeignKey(d => d.TrosakId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kupovina_Trosak");
            });

            modelBuilder.Entity<Prihod>(entity =>
            {
                entity.HasKey(e => e.PrihodId);

                entity.Property(e => e.PrihodId)
                    .HasColumnName("PrihodID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IznosPrihoda).HasColumnType("money");

                entity.Property(e => e.NazivPrihoda)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.VremePrihoda).HasColumnType("date");
            });

            modelBuilder.Entity<Priliv>(entity =>
            {
                entity.HasKey(e => new { e.PrihodId, e.KorisnikId });

                entity.Property(e => e.PrihodId).HasColumnName("PrihodID");

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Prilivi)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Priliv_Korisnik");

                entity.HasOne(d => d.Prihod)
                    .WithMany(p => p.Prilivi)
                    .HasForeignKey(d => d.PrihodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prilivi_Prihodi");
            });

            modelBuilder.Entity<Proizvod>(entity =>
            {
                entity.HasKey(e => e.ProizvodId);

                entity.Property(e => e.ProizvodId)
                    .HasColumnName("ProizvodID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CenaProizvoda).HasColumnType("money");

                entity.Property(e => e.NazivProizvoda)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Trosak>(entity =>
            {
                entity.HasKey(e => e.TrosakId);

                entity.Property(e => e.TrosakId)
                    .HasColumnName("TrosakID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IznosTroska).HasColumnType("money");

                entity.Property(e => e.NazivTroska)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.VremeTroska).HasColumnType("date");
            });

            modelBuilder.Entity<ZeljeniProizvod>(entity =>
            {
                entity.HasKey(e => new { e.ProizvodId, e.KorisnikId });

                entity.Property(e => e.ProizvodId).HasColumnName("ProizvodID");

                entity.Property(e => e.KorisnikId).HasColumnName("KorisnikID");

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.ZeljeniProizvodi)
                    .HasForeignKey(d => d.KorisnikId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZeljeniProizvod_Korisnik");

                entity.HasOne(d => d.Proizvod)
                    .WithMany(p => p.ZeljeniProizvodi)
                    .HasForeignKey(d => d.ProizvodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZeljeniProizvodi_Proizvodi");
            });
        }
    }
}
