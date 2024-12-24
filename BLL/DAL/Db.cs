using System;
using System.Collections.Generic;
using BLL.DAL;
using Microsoft.EntityFrameworkCore;

namespace BLL.Context;

public partial class Db : DbContext
{
    public Db(DbContextOptions<Db> options)
        : base(options)
    {
    }

    public virtual DbSet<director> director { get; set; }

    public virtual DbSet<genre> genre { get; set; }

    public virtual DbSet<movie> movie { get; set; }

    public virtual DbSet<moviegenre> moviegenre { get; set; }

    public virtual DbSet<role> role { get; set; }

    public virtual DbSet<user> user { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<director>(entity =>
        {
            entity.HasKey(e => e.id).HasName("director_pkey");

            entity.Property(e => e.isretired).HasDefaultValue(false);
        });

        modelBuilder.Entity<genre>(entity =>
        {
            entity.HasKey(e => e.id).HasName("genre_pkey");
        });

        modelBuilder.Entity<movie>(entity =>
        {
            entity.HasKey(e => e.id).HasName("movie_pkey");

            entity.Property(e => e.totalrevenue).HasDefaultValueSql("0.00");

            entity.HasOne(d => d.director).WithMany(p => p.movie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_director");
        });

        modelBuilder.Entity<moviegenre>(entity =>
        {
            entity.HasKey(e => e.id).HasName("moviegenre_pkey");

            entity.HasOne(d => d.genre).WithMany(p => p.moviegenre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_genre");

            entity.HasOne(d => d.movie).WithMany(p => p.moviegenre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movie");
        });

        modelBuilder.Entity<role>(entity =>
        {
            entity.HasKey(e => e.id).HasName("role_pkey");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("User_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("nextval('\"User_id_seq\"'::regclass)");
            entity.Property(e => e.isactive).HasDefaultValue(true);

            entity.HasOne(d => d.role).WithMany(p => p.user)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
