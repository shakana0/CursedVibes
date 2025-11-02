using CursedVibes.Domain.Interfaces;

namespace CursedVibes.Tests.Mocks.Scenes
{
    public class FakeBlobSceneRepository : ISceneRepository
    {
        public Task<Stream> GetSceneAsync(string sceneId, CancellationToken cancellationToken)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "Mocks", "Scenes", $"{sceneId}.json");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Scene '{sceneId}' not found at '{filePath}'.");

            var stream = File.OpenRead(filePath);
            return Task.FromResult<Stream>(stream);
        }
    }
}
