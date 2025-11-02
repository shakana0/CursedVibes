using AutoMapper;
using CursedVibes.Application.Scenes.Dtos.Scene;
using CursedVibes.Application.Scenes.Queries;
using CursedVibes.Domain.Entities;
using CursedVibes.Domain.Interfaces;
using CursedVibes.Tests.Mocks.Scenes;
using Moq;

namespace CursedVibes.Tests.Application.ScenesTests.Queries
{
    public class GetSceneByIdHandlerTests
    {
        private readonly ISceneRepository _sceneRepository;
        public GetSceneByIdHandlerTests()
        {
            _sceneRepository = new FakeBlobSceneRepository();
        }

        [Fact]
        public async Task Handle_GivenValidSceneIdQuery_ReturnsSceneDto()
        {
            var query = new GetSceneByIdQuery("scene1");
            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(m => m.Map<SceneDto>(It.IsAny<Scene>()))
                .Returns((Scene s) => SceneTestDataGenerator.CreateSceneDto(query.SceneId)
            );

            var handler = new GetSceneByIdHandler(_sceneRepository, mapperMock.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<SceneDto>(result);
            Assert.Equal("scene1", result.SceneId);
            Assert.Collection(result.Narration,
                line => Assert.Equal("It was a quiet morning...", line),
                line => Assert.Equal("The sun rose slowly over the hills.", line)
            );
            Assert.Single(result.Choices);
            Assert.Equal(10, result.Choices[0].StatImpact.CurseLevel);

            mapperMock.Verify(m => m.Map<SceneDto>(It.IsAny<Scene>()), Times.Once);

        }
    }
}
