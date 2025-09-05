using CursedVibes.Domain.Entities;

namespace CursedVibes.Domain.Interfaces
{
    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllAsync(CancellationToken cancellationToken);
        Task<Character?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task CreateAsync(Character character, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
