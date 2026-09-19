using Microsoft.EntityFrameworkCore;
using URL_Shortener_API.Models;

namespace URL_Shortener_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}

        public DbSet<ShortUrl> ShortUrls { get; set; }
    }
}
