using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using System;

namespace MyApp.Infrastructure.Persistence.EntityFramework
{
    /// <summary>
    /// Datenbank-Context für die DJORGA Anwendung mit Fluent API Mappings.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Track> Tracks => Set<Track>();
        public DbSet<Playlist> Playlists => Set<Playlist>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=djorga.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapping für Track
            modelBuilder.Entity<Track>(entity =>
            {
                entity.ToTable("Tracks");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Artist).HasMaxLength(500);
                entity.Property(e => e.FilePath).IsRequired();
                
                // Index für schnellere Suche
                entity.HasIndex(e => new { e.Title, e.Artist });
            });

            // Mapping für Playlist
            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.ToTable("Playlists");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);

                // Beziehung: Eine Playlist hat viele Tracks (Vereinfacht für MVP)
                // Hinweis: Im echten DJ-Szenario wäre hier oft eine Many-to-Many Beziehung sinnvoll.
                // Für den MVP nutzen wir eine einfache Collection.
                entity.HasMany(e => e.Items)
                      .WithOne()
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
