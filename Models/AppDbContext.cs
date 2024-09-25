using Microsoft.EntityFrameworkCore;

namespace ListApp.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
