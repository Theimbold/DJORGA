using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyApp.Domain.Entities;
using MyApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MyApp.Infrastructure.Persistence.EntityFramework
{
    /// <summary>
    /// Datenbank-Context für die DJORGA Anwendung mit erweiterten Mappings (V3).
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Track> Tracks => Set<Track>();
        public DbSet<Playlist> Playlists => Set<Playlist>();
        public DbSet<SmartCollection> SmartCollections => Set<SmartCollection>();

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

            // JSON Converter für FilterRules (SQLite Support)
            var rulesConverter = new ValueConverter<List<FilterRule>, string>(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<FilterRule>>(v, (JsonSerializerOptions?)null) ?? new List<FilterRule>());

            // Mapping für SmartCollection
            modelBuilder.Entity<SmartCollection>(entity =>
            {
                entity.ToTable("SmartCollections");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Rules).HasConversion(rulesConverter);
            });

            // Mapping für Track (Erweitert um Epic 007 Felder)
            modelBuilder.Entity<Track>(entity =>
            {
                entity.ToTable("Tracks");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(500);
                entity.Property(e => e.Artist).HasMaxLength(500);
                entity.Property(e => e.Genre).HasMaxLength(100);
                entity.Property(e => e.CoverArtPath).HasMaxLength(1000);
                entity.Property(e => e.FilePath).IsRequired();

                // Contextual DNA Mapping (Enums werden standardmäßig als int gespeichert)
                entity.Property(e => e.Mood).HasDefaultValue(TrackMood.None);
                entity.Property(e => e.TimeContext).HasDefaultValue(TrackTimeContext.None);
                
                entity.HasIndex(e => new { e.Title, e.Artist });
            });

            // Mapping für Playlist (Many-to-Many zu Tracks)
            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.ToTable("Playlists");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);

                entity.HasMany(e => e.Items)
                      .WithMany()
                      .UsingEntity(j => j.ToTable("PlaylistTracks"));
            });
        }
    }
}
