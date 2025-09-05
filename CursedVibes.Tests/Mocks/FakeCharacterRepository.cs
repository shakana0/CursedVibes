using CursedVibes.Domain.Entities;
using CursedVibes.Domain.Interfaces;

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
    }
}