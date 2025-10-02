using AutoMapper;
using CursedVibes.Application.Characters.Dtos;
using CursedVibes.Domain.Entities;
using CursedVibes.Domain.Interfaces;
using MediatR;
using System.Text.Json;

namespace CursedVibes.Application.Scenes.Queries
{
    public class GetSceneByIdHandler: IRequestHandler<GetSceneByIdQuery, SceneDto>
    {
        private readonly ISceneRepository _sceneRepository;
        private readonly IMapper _mapper;

        public GetSceneByIdHandler(ISceneRepository sceneRepository, IMapper mapper)
        {
            _sceneRepository = sceneRepository;
            _mapper = mapper;
        }

        public async Task<SceneDto> Handle(GetSceneByIdQuery query, CancellationToken cancellationToken)
        {
            using var stream = await _sceneRepository.GetSceneAsync(query.SceneId, cancellationToken);
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            stream.Position = 0;
            var scene = await JsonSerializer.DeserializeAsync<Scene>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }, cancellationToken);

            if (scene == null)
                throw new InvalidDataException($"Scene '{query.SceneId}' could not be deserialized.");

            return _mapper.Map<SceneDto>(scene);
        }

    }
}
