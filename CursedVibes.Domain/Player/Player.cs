using CursedVibes.Domain.ValueObjects;

namespace CursedVibes.Domain.Player
{
    public class Player
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public PlayerNarrativeState PlayerNarrativeState { get; private set; } = new PlayerNarrativeState();

        // Optional: future login support
        public string? Email { get; private set; }
        public string? Username { get; private set; }

        // Optional: methods to link login later
        public void Register(string email, string username)
        {
            Email = email;
            Username = username;
        }

    }
}
