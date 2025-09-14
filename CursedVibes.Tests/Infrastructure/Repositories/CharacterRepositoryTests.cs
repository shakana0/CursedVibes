using AutoMapper;
using CursedVibes.Application.Characters.Commands.DeleteCharacter;
using CursedVibes.Application.Characters.Queries.GetCharacter;
using CursedVibes.Application.Characters.Queries.SearchCharacters;
using CursedVibes.Application.Exceptions;
using CursedVibes.Application.Infrastructure.AutoMapper;
using CursedVibes.Domain.Characters.Filters;
using CursedVibes.Domain.Entities;
using CursedVibes.Infrastructure.Repositories;
using CursedVibes.Tests.Mocks;
using Microsoft.Extensions.Logging;

namespace CursedVibes.Tests.Infrastructure.Repositories
{
    public class CharacterRepositoryTests
    {
        public static IMapper Create()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            }, loggerFactory);

            return config.CreateMapper();
        }

        //GetById
        [Fact]
        public async Task GetByIdAsync_ShouldReturnCharacter_WhenIdExists()
        {
            var context = TestDbContextFactory.Create();
            var characterRepository = new CharacterRepository(context);

            var query = new GetCharacterByIdQuery(2);

            var result = await characterRepository.GetByIdAsync(query.Id, CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<Character>(result);
            Assert.Equal(result.Id, query.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenCharacterDoesNotExist()
        {
            var context = TestDbContextFactory.Create();
            var characterRepository = new CharacterRepository(context);

            var query = new GetCharacterByIdQuery(-999);

            var result = await characterRepository.GetByIdAsync(query.Id, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrow_WhenCancelled()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            var repository = new CharacterRepository(context);
            var cts = new CancellationTokenSource();
            cts.Cancel(); // signals that the token is canceled

            // Act & Assert
            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                await repository.GetByIdAsync(2, cts.Token);
            });
        }

        //Create

        [Fact]
        public async Task CreateAsync_ShouldSaveCharacterWithCorrectProperties()
        {
            var context = TestDbContextFactory.Create();
            var characterRepository = new CharacterRepository(context);

            var command = TestDataGenerator.CreateCharacterCommand(name: "Test 4");

            var mapper = Create();
            var character = mapper.Map<Character>(command);
            character.UpdateVibeType(command.VibeType);

            await characterRepository.CreateAsync(character, CancellationToken.None);

            var savedCharacter = await characterRepository.GetByIdAsync(4, CancellationToken.None);

            Assert.NotNull(savedCharacter);
            Assert.IsType<Character>(savedCharacter);
            Assert.Contains(savedCharacter, context.Characters); //Confirms that the entity is part of the DbSet
            Assert.Equal(4, savedCharacter.Id);
            Assert.Equal(savedCharacter.Name, command.Name);
            Assert.Equal(command.BackStory, savedCharacter.BackStory);
            Assert.Equal(command.VibeType, savedCharacter.VibeType);
        }

        //Update

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCharacterWithCorrectProperties()
        {
            var context = TestDbContextFactory.Create();
            var characterRepository = new CharacterRepository(context);

            var command = TestDataGenerator.UpdateCharacterCommand(
                name: "Test Updated",
                vibeType: "Chaotic, Creepy",
                curseLevel: 85,
                backStory: "Forged in the shadows of forgotten algorithms.",
                strength: 17,
                agility: 23,
                intelligence: 42,
                luck: 7
                );

            var character = await characterRepository.GetByIdAsync(command.Id, CancellationToken.None);
            if (character == null)
                throw new NotFoundException("Character", $"{command.Id}");

            character.UpdateName(command.Name);
            character.UpdateCurseLevel(command.CurseLevel);
            character.UpdateVibeType(command.VibeType);
            character.UpdateBackStory(command.BackStory);
            character.UpdateStats(
                command.Strength,
                command.Agility,
                command.Intelligence,
                command.Luck);

            await characterRepository.SaveChangesAsync(CancellationToken.None);

            var updatedCharacter = await characterRepository.GetByIdAsync(command.Id, CancellationToken.None);

            Assert.NotNull(updatedCharacter);
            Assert.Contains(updatedCharacter, context.Characters);
            Assert.Equal(command.Id, updatedCharacter.Id);
            Assert.Equal(command.Name, updatedCharacter.Name);
            Assert.Equal(command.VibeType, updatedCharacter.VibeType);
            Assert.Equal(command.CurseLevel, updatedCharacter.CurseLevel);
            Assert.Equal(command.BackStory, updatedCharacter.BackStory);
            Assert.Equal(command.Strength, updatedCharacter.Stats.Strength);
            Assert.Equal(command.Agility, updatedCharacter.Stats.Agility);
            Assert.Equal(command.Intelligence, updatedCharacter.Stats.Intelligence);
            Assert.Equal(command.Luck, updatedCharacter.Stats.Luck);

        }

        //Delete

        [Fact]
        public async Task DeleteAsync_ShouldReturnNull_WhenCharacterIsDeleted()
        {
            var context = TestDbContextFactory.Create();
            var characterRepository = new CharacterRepository(context);

            var command = new DeleteCharacterCommand(2);

            var result = await characterRepository.DeleteAsync(command.Id, CancellationToken.None);

            var deletedCharacter = await characterRepository.GetByIdAsync(command.Id, CancellationToken.None);

            Assert.True(result);
            Assert.Null(deletedCharacter);
            Assert.DoesNotContain(context.Characters, c => c.Id == command.Id);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenCharacterIdDoesNotExcist()
        {
            var context = TestDbContextFactory.Create();
            var characterRepository = new CharacterRepository(context);

            var command = new DeleteCharacterCommand(-999);

            var result = await characterRepository.DeleteAsync(command.Id, CancellationToken.None);

            Assert.False(result);
        }

        //Filter

        [Fact]
        public async Task SearchAsync_ShouldReturnMatchingCharacters_WhenFilterHasValidCriteria()
        {
            var context = TestDbContextFactory.Create();
            var characterRepository = new CharacterRepository(context);

            var query = new SearchCharactersQuery()
            {
                Name = "Character 3"
            };

            var mapper = Create();
            var searchParam = mapper.Map<CharacterSearchFilter>(query);

            var result = await characterRepository.SearchAsync(searchParam, CancellationToken.None);

            Assert.NotEmpty(result);
            Assert.Single(result);
            Assert.Contains(query.Name, result.First().Name);
        }

        [Fact]
        public async Task SearchAsync_ShouldReturnEmptyList_WhenFilterHasInvalidCriteria()
        {
            var context = TestDbContextFactory.Create();
            var characterRepository = new CharacterRepository(context);

            var query = new SearchCharactersQuery()
            {
                Name = "Nonexistent"
            };

            var mapper = Create();
            var searchParam = mapper.Map<CharacterSearchFilter>(query);

            var result = await characterRepository.SearchAsync(searchParam, CancellationToken.None);

            Assert.Empty(result);
        }

        [Fact]
        public async Task CountAsync_ShouldReturnCorrectCount_WhenFilterIsApplied()
        {
            var context = TestDbContextFactory.Create();
            var characterRepository = new CharacterRepository(context);

            var query = new SearchCharactersQuery()
            {
                Name = "Character 3"
            };

            var mapper = Create();
            var searchParam = mapper.Map<CharacterSearchFilter>(query);

            var result = await characterRepository.CountAsync(searchParam, CancellationToken.None);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task CountAsync_ShouldReturnZero_WhenNoCharactersMatchFilter()
        {
            var context = TestDbContextFactory.Create();
            var characterRepository = new CharacterRepository(context);

            var query = new SearchCharactersQuery
            {
                Name = "Nonexistent"
            };

            var mapper = Create();
            var searchParam = mapper.Map<CharacterSearchFilter>(query);

            var result = await characterRepository.CountAsync(searchParam, CancellationToken.None);

            Assert.Equal(0, result);
        }
    }
}
