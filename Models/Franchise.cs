
using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models
{
    public class Franchise
    {
        // Auto-incremented Id for the franchise
        [Key]
        public int Id { get; set; }

        // Name of the franchise, required and max length of 100 characters
        [Required, MaxLength(100)]
        public string Title { get; set; }

        // Description of the franchise, optional and max length of 500 characters
        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation property for movies in the franchise
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}


