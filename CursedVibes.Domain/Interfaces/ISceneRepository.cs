namespace CursedVibes.Domain.Interfaces
{
    public interface ISceneRepository
    {
        Task<Stream> GetSceneAsync(string sceneId, CancellationToken cancellationToken);
    }
}
