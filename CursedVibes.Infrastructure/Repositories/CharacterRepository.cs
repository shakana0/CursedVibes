using CursedVibes.Domain.Entities;
using CursedVibes.Domain.Interfaces;
using CursedVibes.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CursedVibes.Infrastructure.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly CursedVibesDbContext _context;

        public CharacterRepository(CursedVibesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Character>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Characters
                .AsNoTracking() // Disable change tracking for better performance
                .ToListAsync(cancellationToken);
        }
        public async Task<Character?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
        public async Task CreateAsync(Character character, CancellationToken cancellationToken)
        {
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existingCharacter = await GetByIdAsync(id, cancellationToken);
            if (existingCharacter == null)
            {
                return false;
            }
            _context.Characters.Remove(existingCharacter);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
