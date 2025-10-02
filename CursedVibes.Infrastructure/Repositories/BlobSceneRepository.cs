using Azure.Storage.Blobs;
using CursedVibes.Domain.Interfaces;

namespace CursedVibes.Infrastructure.Repositories
{
    public class BlobSceneRepository: ISceneRepository
    {
        private readonly BlobContainerClient _container;

        public BlobSceneRepository(BlobContainerClient container)
        {
            _container = container;
        }

        public async Task<Stream> GetSceneAsync(string sceneId, CancellationToken cancellationToken)
        {
            var blob = _container.GetBlobClient($"{sceneId}.json");

            if (!await blob.ExistsAsync())
                throw new FileNotFoundException($"Scene '{sceneId}' not found.");

            return await blob.OpenReadAsync();
        }
    }
}
