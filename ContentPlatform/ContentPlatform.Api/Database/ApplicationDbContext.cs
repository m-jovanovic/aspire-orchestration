using ContentPlatform.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContentPlatform.Api.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("content");
    }

    public DbSet<Article> Articles { get; set; }
}
