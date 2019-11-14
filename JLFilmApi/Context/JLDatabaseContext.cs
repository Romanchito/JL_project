using System;
using JLFilmApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JLFilmApi.Context
{
    public partial class JLDatabaseContext : DbContext
    {
        public JLDatabaseContext()
        {
        }

        public JLDatabaseContext(DbContextOptions<JLDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Films> Films { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=RTRETYAKOV\\SQLEXPRESS;Database=JLDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK_Comments_Reviews");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_Users");
            });

            modelBuilder.Entity<Films>(entity =>
            {
                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Director)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ReleaseDate).HasColumnType("date");

                entity.Property(e => e.Stars).IsRequired();

                entity.Property(e => e.WorldwideGross).HasColumnType("money");
            });

            modelBuilder.Entity<Likes>(entity =>
            {
                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.ReviewId)
                    .HasConstraintName("FK_Likes_Reviews");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Likes_Users");
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.FilmId)
                    .HasConstraintName("FK_Reviews_Films");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Reviews_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
