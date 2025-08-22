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
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
        public async Task CreateAsync(Character character, CancellationToken cancellationToken)
        {
            await _context.Characters.AddAsync(character);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(int id, Character character, CancellationToken cancellationToken)
        {
            var existingCharacter = await GetByIdAsync(id, cancellationToken);
            if (existingCharacter == null)
                return;

            existingCharacter.UpdateName(character.Name);
            existingCharacter.UpdateCurseLevel(character.CurseLevel);
            existingCharacter.UpdateVibeType(character.VibeType);
            existingCharacter.UpdateBackStory(character.BackStory);
            existingCharacter.UpdateStats(
                character.Stats.Strength,
                character.Stats.Agility,
                character.Stats.Intelligence,
                character.Stats.Luck);

            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var character = await GetByIdAsync(id, cancellationToken);
            if (character != null)
            {
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

    }
}
