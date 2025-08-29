using CursedVibes.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CursedVibes.Tests.Mocks
{
    public static class TestDbContextFactory
    {
        public static CursedVibesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CursedVibesDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
                .Options;

            var context = new CursedVibesDbContext(options);

            // Optional: Seed initial data
            context.Characters.AddRange(TestDataGenerator.CreateCharacterList(3));
            context.SaveChanges();

            return context;
        }
    }
}
