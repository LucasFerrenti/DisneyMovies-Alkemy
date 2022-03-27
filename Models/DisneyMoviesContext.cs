using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy.Models
{
    public class DisneyMoviesContext : DbContext
    {
        public DisneyMoviesContext(DbContextOptions<DisneyMoviesContext> options) : base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participation>().HasKey(p => new { p.MovieId, p.CharacterId});
            modelBuilder.Entity<Movie>()
                .HasIndex(u => u.Title)
                .IsUnique();
            modelBuilder.Entity<Genre>()
                .HasIndex(u => u.Name)
                .IsUnique();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Genre> genres { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
