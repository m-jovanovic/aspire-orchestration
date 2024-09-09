using ContentPlatform.Reporting.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContentPlatform.Reporting.Api.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("reporting");

        modelBuilder.Entity<ArticleEvent>().HasOne<Article>().WithMany();
    }

    public DbSet<Article> Articles { get; set; }

    public DbSet<ArticleEvent> ArticleEvents { get; set; }
}
