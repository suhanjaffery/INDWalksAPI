using IndWalksAPI.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace IndWalksAPI.Data
{
    public class INDWalksDbContext : DbContext
    {
        public INDWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> difficulties { get; set; }
        public DbSet<Region> regions { get; set; }
        public DbSet<Walks> walks { get; set; }
    }
}
