using CursedVibes.Application.Scenes.Dtos.Scene;
using MediatR;

namespace CursedVibes.Application.Scenes.Queries
{
    public class GetSceneByIdQuery: IRequest<SceneDto>
    {
        public string SceneId { get; set; } = string.Empty;

        public GetSceneByIdQuery(string sceneId)
        {
            SceneId = sceneId;
        }
    }
}
