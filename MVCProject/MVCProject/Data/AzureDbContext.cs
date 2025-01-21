using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;

namespace MVCProject.Data
{
    public class AzureDbContext : IdentityDbContext<Gebruiker>
    {

        public AzureDbContext(DbContextOptions<AzureDbContext> options) : base(options) { }

        public DbSet<Klas> Klassen { get; set; } = null!;
        public DbSet<Studiebezoek> Studiebezoeken { get; set; } = null!;
        public DbSet<KlasStudiebezoek> KlasStudiebezoeken { get; set; } = null!;
        public DbSet<Afwezigheid> Afwezigheden { get; set; } = null!;
        public DbSet<Begeleiding> Begeleidingen { get; set; } = null!;
        public DbSet<Bijlage> Bijlages { get; set; } = null!;
        public DbSet<Foto> Fotos { get; set; } = null!;
        public DbSet<FotoAlbum> FotoAlbums { get; set; } = null!;
        public DbSet<Gebruiker> Gebruikers { get; set; } = null!;
        public DbSet<GebruikerNavorming> GebruikerNavormingen { get; set; } = null!;
        public DbSet<Navorming> Navormingen { get; set; } = null!;
        public DbSet<Vak> Vakken { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Klas>().ToTable("Klas");
            modelBuilder.Entity<Studiebezoek>().ToTable("Studiebezoek");
            modelBuilder.Entity<KlasStudiebezoek>().ToTable("KlasStudiebezoek");
            modelBuilder.Entity<Afwezigheid>().ToTable("Afwezigheid");
            modelBuilder.Entity<Begeleiding>().ToTable("Begeleiding");
            modelBuilder.Entity<Bijlage>().ToTable("Bijlage");
            modelBuilder.Entity<Foto>().ToTable("Foto");
            modelBuilder.Entity<FotoAlbum>().ToTable("FotoAlbum");
            modelBuilder.Entity<Gebruiker>().ToTable("Gebruiker");
            modelBuilder.Entity<GebruikerNavorming>().ToTable("GebruikerNavorming");
            modelBuilder.Entity<Navorming>().ToTable("Navorming");
            modelBuilder.Entity<Vak>().ToTable("Vak");



            /////// One to many /////////

            modelBuilder.Entity<Studiebezoek>()
                .HasOne(p => p.Vak)
                .WithMany(x => x.Studiebezoeken)
                .HasForeignKey(y => y.VakId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Studiebezoek>()
                .HasOne(p => p.Aanvrager)
                .WithMany(x => x.StudiebezoekAanvragen)
                .HasForeignKey(y => y.AanvragerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Navorming>()
                .HasOne(p => p.Aanvrager)
                .WithMany(x => x.NavormingAanvragen)
                .HasForeignKey(y => y.AanvragerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Foto>()
                .HasOne(p => p.FotoAlbum)
                .WithMany(x => x.Fotos)
                .HasForeignKey(y => y.FotoAlbumId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FotoAlbum>()
                .HasOne(p => p.Studiebezoek)
                .WithMany(x => x.FotoAlbums)
                .HasForeignKey(y => y.StudiebezoekId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Afwezigheid>()
                .HasOne(p => p.Gebruiker)
                .WithMany(x => x.Afwezigheden)
                .HasForeignKey(y => y.GebruikerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bijlage>()
                .HasOne(p => p.Studiebezoek)
                .WithMany(x => x.Bijlages)
                .HasForeignKey(y => y.StudiebezoekId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            ////// Many to many ////////

            //Studiebezoek-KlasStudiebezoek-Klas
            modelBuilder.Entity<Studiebezoek>()
                .HasMany(p => p.Klassen)
                .WithMany(x => x.Studiebezoeken)
                .UsingEntity<KlasStudiebezoek>(
                    l=> l.HasOne<Klas>().WithMany().HasForeignKey(e=>e.KlasId).IsRequired().OnDelete(DeleteBehavior.Restrict),
                    r=> r.HasOne<Studiebezoek>().WithMany().HasForeignKey(e=>e.StudiebezoekId).IsRequired().OnDelete(DeleteBehavior.Restrict)
                );

            //Studiebezoek - Begeleiding - Gebruiker
            modelBuilder.Entity<Studiebezoek>()
                .HasMany(p => p.Begeleiders)
                .WithMany(x => x.StudiebezoekBegeleidingen)
                .UsingEntity<Begeleiding>(
                    l => l.HasOne<Gebruiker>().WithMany().HasForeignKey(e => e.GebruikerId).IsRequired().OnDelete(DeleteBehavior.Restrict),
                    r => r.HasOne<Studiebezoek>().WithMany().HasForeignKey(e => e.StudiebezoekId).IsRequired().OnDelete(DeleteBehavior.Restrict)
                );

            //Gebruiker - GebruikerNavorming - Navorming
            modelBuilder.Entity<Gebruiker>()
                .HasMany(p => p.NavormingDeelnames)
                .WithMany(x => x.Deelnemers)
                .UsingEntity<GebruikerNavorming>(
                    l => l.HasOne<Navorming>().WithMany().HasForeignKey(e => e.NavormingId).IsRequired().OnDelete(DeleteBehavior.Restrict),
                    r => r.HasOne<Gebruiker>().WithMany().HasForeignKey(e => e.DeelnemerId).IsRequired().OnDelete(DeleteBehavior.Restrict)
                );

            /////// Requirements /////////

            modelBuilder.Entity<Studiebezoek>().Property(p => p.Datum).HasColumnType("date");
            //modelBuilder.Entity<Studiebezoek>().Property(p => p.StartUur).HasColumnType("time");
            //modelBuilder.Entity<Studiebezoek>().Property(p => p.EindUur).HasColumnType("time");
            modelBuilder.Entity<Studiebezoek>().Property(p => p.KostprijsStudiebezoek).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Studiebezoek>().Property(p => p.KostprijsVervoer).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Navorming>().Property(p => p.Datum).HasColumnType("date");
            //modelBuilder.Entity<Navorming>().Property(p => p.StartUur).HasColumnType("time");
            //modelBuilder.Entity<Navorming>().Property(p => p.EindUur).HasColumnType("time");
            modelBuilder.Entity<Navorming>().Property(p => p.Kostprijs).HasColumnType("decimal(18,2)");

        }

    }
}
