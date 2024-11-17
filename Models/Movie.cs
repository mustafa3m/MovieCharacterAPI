using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace MovieCharacterAPI.Models
{
    public class Movie
    {
        // Auto-incremented Id for the movie
        [Key]
        public int Id { get; set; }

        // Movie title, required and max length of 150 characters
        [Required, MaxLength(150)]
        public string Title { get; set; }

        // Genre of the movie, as a comma-separated string, max length of 100 characters
        [MaxLength(100)]
        public string Genre { get; set; }

        // Release year of the movie
        public int ReleaseYear { get; set; }

        // Director's name, max length of 100 characters
        [MaxLength(100)]
        
        public string? Director { get; set; }
      

        // URL to the movie poster, optional
        [Url]
        public string? Picture { get; set; }

        // URL to the movie trailer, optional
        [Url]
        public string? Trailer { get; set; }

        // Foreign key for the franchise the movie belongs to
        public int FranchiseId { get; set; }

        // Navigation property for the franchise this movie belongs to
        public Franchise Franchise { get; set; }

        // Navigation property for characters in the movie
        public ICollection<Character> Characters { get; set; } = new List<Character>();
    }
}


