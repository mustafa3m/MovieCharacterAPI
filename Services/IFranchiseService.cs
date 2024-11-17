using MovieCharacterAPI.Models;

namespace MovieCharacterAPI.Services
{
    public interface IFranchiseService
    {
        // Retrieve all franchises
        Task<IEnumerable<Franchise>> GetAllFranchisesAsync();
        // Retrieve a franchise by its ID
        Task<Franchise> GetFranchiseByIdAsync(int id);
        // Create a new franchise
        Task<Franchise> CreateFranchiseAsync(Franchise franchise);
        // Update an existing franchise
        Task<Franchise> UpdateFranchiseAsync(Franchise franchise);
        // Delete a franchise by its ID
        Task<bool> DeleteFranchiseAsync(int id);
    }
}
