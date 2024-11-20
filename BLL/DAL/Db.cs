using System;
using System.Collections.Generic;
using BLL.DAL;
using Microsoft.EntityFrameworkCore;
namespace BLL.DAL;

public partial class Db : DbContext
{
    public Db(DbContextOptions<Db> options)
        : base(options)
    {
    }
    public DbSet<director> director { get; set; }

    public  DbSet<genre> genre { get; set; }

    public  DbSet<movie> movie { get; set; }

    public  DbSet<moviegenre> moviegenre { get; set; }

    public  DbSet<role> role { get; set; }

    public  DbSet<user> user { get; set; }

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

            entity.HasOne(d => d.director).WithMany(p => p.movie)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("movie_directorid_fkey");
        });

        modelBuilder.Entity<moviegenre>(entity =>
        {
            entity.HasKey(e => e.id).HasName("moviegenre_pkey");

            entity.HasOne(d => d.genre).WithMany(p => p.moviegenre)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("moviegenre_genreid_fkey");

            entity.HasOne(d => d.movie).WithMany(p => p.moviegenre)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("moviegenre_movieid_fkey");
        });

        modelBuilder.Entity<role>(entity =>
        {
            entity.HasKey(e => e.id).HasName("role_pkey");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("user_pkey");

            entity.Property(e => e.isactive).HasDefaultValue(true);

            entity.HasOne(d => d.role).WithMany(p => p.user)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("user_roleid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
