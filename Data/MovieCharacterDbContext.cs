using MovieCharacterAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MovieCharacterAPI.Data
{
    public class MovieCharacterDbContext : DbContext
    {
        public MovieCharacterDbContext(DbContextOptions<MovieCharacterDbContext> options) : base(options) { }

        // DbSet properties for each model
        public DbSet<Character> Characters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Franchise> Franchises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship between Character and Movie
            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Characters)
                .WithMany(c => c.Movies);

            // Configure one-to-many relationship between Franchise and Movie
            modelBuilder.Entity<Franchise>()
                .HasMany(f => f.Movies)
                .WithOne(m => m.Franchise)
                .HasForeignKey(m => m.FranchiseId);

            // Seed data for testing
            modelBuilder.Seed();
        }
    }

    // Extension class for ModelBuilder to define the Seed method
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Franchise>().HasData(
                new Franchise { Id = 1, Title = "Marvel Cinematic Universe", Description = "Superhero movies." },
                new Franchise { Id = 2, Title = "The Lord of the Rings", Description = "Fantasy movies based on Tolkien's books." }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Iron Man", Genre = "Action, Adventure", ReleaseYear = 2008, Director = "Jon Favreau", FranchiseId = 1 },
                new Movie { Id = 2, Title = "The Fellowship of the Ring", Genre = "Fantasy, Adventure", ReleaseYear = 2001, Director = "Peter Jackson", FranchiseId = 2 }
            );

            modelBuilder.Entity<Character>().HasData(
                new Character { Id = 1, FullName = "Tony Stark", Alias = "Iron Man", Gender = "Male", Picture = "https://example.com/ironman.jpg" },
                new Character { Id = 2, FullName = "Frodo Baggins", Gender = "Male", Picture = "https://example.com/frodo.jpg" }
            );
        }
    }
}
