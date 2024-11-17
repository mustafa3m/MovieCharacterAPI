using MovieCharacterAPI.Models;

namespace MovieCharacterAPI.Services
{
    public interface ICharacterService
    {
        // Retrieve all characters
        Task<IEnumerable<Character>> GetAllCharactersAsync();
        // Retrieve a character by its ID
        Task<Character> GetCharacterByIdAsync(int id);
        // Create a new character
        Task<Character> CreateCharacterAsync(Character character);
        // Update an existing character
        Task<Character> UpdateCharacterAsync(Character character);
        // Delete a character by its ID
        Task<bool> DeleteCharacterAsync(int id);

        // Method to check if a character exists
        Task<bool> CharacterExistsAsync(int id);
    }
}
