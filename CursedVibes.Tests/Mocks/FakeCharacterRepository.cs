using CursedVibes.Domain.Characters.Filters;
using CursedVibes.Domain.Entities;
using CursedVibes.Domain.Interfaces;
using CursedVibes.Domain.Characters.Extensions;

namespace CursedVibes.Tests.Mocks
{
    public class FakeCharacterRepository : ICharacterRepository
    {
        private readonly List<Character> _characters = new();

        public Task<IEnumerable<Character>> GetAllAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult<IEnumerable<Character>>(_characters.ToList());
        }

        public Task<Character?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var character = _characters.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(character);
        }

        public Task CreateAsync(Character character, CancellationToken cancellationToken)
        {
            _characters.Add(character);
            return Task.CompletedTask;
        }

        public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var character = _characters.FirstOrDefault(c => c.Id == id);
            if (character == null)
            {
                return Task.FromResult(false);
            }
            _characters.Remove(character);

            return Task.FromResult(true);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Character>> SearchAsync(CharacterSearchFilter filter, CancellationToken cancellationToken)
        {
            var result = _characters
                .AsQueryable()
                .ApplySearchFilter(filter)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize);

            return Task.FromResult<IEnumerable<Character>>(result.ToList());
        }

        public Task<int> CountAsync(CharacterSearchFilter filter, CancellationToken cancellationToken)
        {
            var result = _characters
                            .AsQueryable()
                            .ApplySearchFilter(filter);
            
            var count = result.Count();
            return Task.FromResult(count);
        }

    }
}