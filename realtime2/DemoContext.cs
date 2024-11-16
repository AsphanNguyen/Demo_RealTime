using Microsoft.EntityFrameworkCore;
using realtime2.Models;
namespace realtime2
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options):base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
