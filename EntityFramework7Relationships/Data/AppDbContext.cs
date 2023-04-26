using EntityFramework7Relationships.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework7Relationships.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Backpack> Backpacks => Set<Backpack>();
        public DbSet<Character> Characters => Set<Character>();
        public DbSet<Faction> Factions => Set<Faction>();
        public DbSet<Weapon> Weapons => Set<Weapon>();

    }
}
