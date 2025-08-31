using Microsoft.EntityFrameworkCore;
using CursedVibes.Domain.Entities;

namespace CursedVibes.Infrastructure.Context
{
    public class CursedVibesDbContext : DbContext
    {
        public CursedVibesDbContext(DbContextOptions<CursedVibesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Fluent API -> config
            //Map Character to table "Character" in schema "dbo"
            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("Character", "dbo");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id)
                        .ValueGeneratedOnAdd(); //säger att Id genereras av databasen

                //Define relationships
                entity.OwnsOne(c => c.Stats, stats =>
                {
                    stats.Property(s => s.Strength).HasColumnName("Strength");
                    stats.Property(s => s.Agility).HasColumnName("Agility");
                    stats.Property(s => s.Intelligence).HasColumnName("Intelligence");
                    stats.Property(s => s.Luck).HasColumnName("Luck");
                });
            });
        }
    }
}
