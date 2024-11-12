using System.ComponentModel.DataAnnotations;

namespace MovieCharacterAPI.Models
{
    public class Character
    {
        // Auto-incremented Id for the character
        [Key]
        public int Id { get; set; }

        // Character's full name, required and max length of 100 characters
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        // Alias of the character, optional and max length of 50 characters
        [MaxLength(50)]
        public string? Alias { get; set; }

        // Gender of the character, required and max length of 10 characters
        [Required, MaxLength(10)]
        public string Gender { get; set; }

        // URL to the character's picture, optional
        [Url]
        public string? Picture { get; set; }

        // Navigation property for movies the character appears in
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}

