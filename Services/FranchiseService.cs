using MovieCharacterAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using MovieCharacterAPI.Data;

namespace MovieCharacterAPI.Services
{
   

    public class FranchiseService : IFranchiseService
    {
        private readonly MovieCharacterDbContext _context;

        public FranchiseService(MovieCharacterDbContext context)
        {
            _context = context;
        }

        // Retrieve all franchises from the database
        public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
        {
            return await _context.Franchises.Include(f => f.Movies).ToListAsync();
        }

        // Retrieve a specific franchise by its ID
        public async Task<Franchise> GetFranchiseByIdAsync(int id)
        {
            return await _context.Franchises.Include(f => f.Movies).FirstOrDefaultAsync(f => f.Id == id);
        }

        // Create a new franchise
        public async Task<Franchise> CreateFranchiseAsync(Franchise franchise)
        {
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }

        // Update an existing franchise
        public async Task<Franchise> UpdateFranchiseAsync(Franchise franchise)
        {
            _context.Entry(franchise).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FranchiseExists(franchise.Id))
                {
                    return null;
                }
                throw;
            }
            return franchise;
        }

        // Delete a franchise by its ID
        public async Task<bool> DeleteFranchiseAsync(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            if (franchise == null)
            {
                return false;
            }

            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
            return true;
        }

        // Check if a franchise exists by its ID
        private bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(e => e.Id == id);
        }
    }
}
