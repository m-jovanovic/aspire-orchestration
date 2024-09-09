using ContentPlatform.Reporting.Api.Database;
using ContentPlatform.Reporting.Api.Entities;
using Contracts;
using MassTransit;

namespace ContentPlatform.Reporting.Api.Articles;

public sealed class ArticleCreatedConsumer : IConsumer<ArticleCreatedEvent>
{
    private readonly ApplicationDbContext _context;

    public ArticleCreatedConsumer(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<ArticleCreatedEvent> context)
    {
        var article = new Article
        {
            Id = context.Message.Id,
            CreatedOnUtc = context.Message.CreatedOnUtc
        };

        _context.Add(article);

        await _context.SaveChangesAsync();
    }
}
