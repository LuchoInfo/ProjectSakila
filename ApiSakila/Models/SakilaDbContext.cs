using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ApiSakila.Models.Dto;

namespace ApiSakila.Models;

public partial class SakilaDbContext : DbContext
{
    public SakilaDbContext()
    {
    }

    public SakilaDbContext(DbContextOptions<SakilaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmActor> FilmActors { get; set; }

    public virtual DbSet<FilmCategory> FilmCategories { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorId).HasName("PK__actor__8B2447B4F8084E0E");

            entity.ToTable("actor");

            entity.HasIndex(e => e.LastName, "idx_actor_last_name");

            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_update");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__category__D54EE9B447FBF225");

            entity.ToTable("category");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("category_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_update");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.FilmId).HasName("PK__film__349764A92C6DBC08");

            entity.ToTable("film");

            entity.HasIndex(e => e.LanguageId, "idx_fk_language_id");

            entity.HasIndex(e => e.OriginalLanguageId, "idx_fk_original_language_id");

            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("(NULL)")
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_update");
            entity.Property(e => e.Length)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("length");
            entity.Property(e => e.OriginalLanguageId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("original_language_id");
            entity.Property(e => e.Rating)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("G")
                .HasColumnName("rating");
            entity.Property(e => e.ReleaseYear)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("release_year");
            entity.Property(e => e.RentalDuration)
                .HasDefaultValue((byte)3)
                .HasColumnName("rental_duration");
            entity.Property(e => e.RentalRate)
                .HasDefaultValue(4.99m)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("rental_rate");
            entity.Property(e => e.ReplacementCost)
                .HasDefaultValue(19.99m)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("replacement_cost");
            entity.Property(e => e.SpecialFeatures)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("special_features");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Language).WithMany(p => p.FilmLanguages)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_film_language");

            entity.HasOne(d => d.OriginalLanguage).WithMany(p => p.FilmOriginalLanguages)
                .HasForeignKey(d => d.OriginalLanguageId)
                .HasConstraintName("fk_film_language_original");
        });

        modelBuilder.Entity<FilmActor>(entity =>
        {
            entity.HasKey(e => new { e.ActorId, e.FilmId }).HasName("PK__film_act__086D31FEACA7C136");

            entity.ToTable("film_actor");

            entity.HasIndex(e => e.ActorId, "idx_fk_film_actor_actor");

            entity.HasIndex(e => e.FilmId, "idx_fk_film_actor_film");

            entity.Property(e => e.ActorId).HasColumnName("actor_id");
            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_update");

            entity.HasOne(d => d.Actor).WithMany(p => p.FilmActors)
                .HasForeignKey(d => d.ActorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_film_actor_actor");

            entity.HasOne(d => d.Film).WithMany(p => p.FilmActors)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_film_actor_film");
        });

        modelBuilder.Entity<FilmCategory>(entity =>
        {
            entity.HasKey(e => new { e.FilmId, e.CategoryId }).HasName("PK__film_cat__69C38A32426EE408");

            entity.ToTable("film_category");

            entity.HasIndex(e => e.CategoryId, "idx_fk_film_category_category");

            entity.HasIndex(e => e.FilmId, "idx_fk_film_category_film");

            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_update");

            entity.HasOne(d => d.Category).WithMany(p => p.FilmCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_film_category_category");

            entity.HasOne(d => d.Film).WithMany(p => p.FilmCategories)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_film_category_film");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.LanguageId).HasName("PK__language__804CF6B399E95940");

            entity.ToTable("language");

            entity.Property(e => e.LanguageId)
                .ValueGeneratedOnAdd()
                .HasColumnName("language_id");
            entity.Property(e => e.LastUpdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("last_update");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

public DbSet<ApiSakila.Models.Dto.FilmDto> FilmDto { get; set; } = default!;
}
