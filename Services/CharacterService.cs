using MovieCharacterAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using MovieCharacterAPI.Data;

namespace MovieCharacterAPI.Services
{
   

    public class CharacterService : ICharacterService
    {
        private readonly MovieCharacterDbContext _context;

        public CharacterService(MovieCharacterDbContext context)
        {
            _context = context;
        }

        // Retrieve all characters from the database
        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters.Include(c => c.Movies).ToListAsync();
        }

        // Retrieve a specific character by its ID
        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            return await _context.Characters.Include(c => c.Movies).FirstOrDefaultAsync(c => c.Id == id);
        }

        // Create a new character
        public async Task<Character> CreateCharacterAsync(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }

        // Update an existing character
        public async Task<Character> UpdateCharacterAsync(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(character.Id))
                {
                    return null;
                }
                throw;
            }
            return character;
        }

        // Delete a character by its ID
        public async Task<bool> DeleteCharacterAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return false;
            }

            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            return true;
        }

        // Check if a character exists by its ID
        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }

        // Check if a character exists
        public async Task<bool> CharacterExistsAsync(int id)
        {
            return await _context.Characters.AnyAsync(e => e.Id == id);
        }
    }
}
