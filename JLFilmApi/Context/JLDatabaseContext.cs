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

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
