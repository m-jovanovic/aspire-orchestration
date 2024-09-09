using Carter;
using ContentPlatform.Api.Database;
using Contracts;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace ContentPlatform.Api.Articles;

public static class DeleteArticle
{
    public class Command : IRequest<Result>
    {
        public Guid Id { get; set; }
    }

    internal sealed class Handler : IRequestHandler<Command, Result>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPublishEndpoint _publishEndpoint;

        public Handler(ApplicationDbContext dbContext, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var article = await _dbContext
                .Articles
                .Where(article => article.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (article is null)
            {
                return Result.Failure(new Error(
                    "GetArticle.Null",
                    "The article with the specified ID was not found"));
            }

            _dbContext.Remove(article);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await _publishEndpoint.Publish(new ArticleDeletedEvent(article.Id), cancellationToken);

            return Result.Success();
        }
    }
}

public class DeleteArticleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/articles/{id}", async (Guid id, ISender sender) =>
        {
            var query = new DeleteArticle.Command { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NotFound(result.Error);
            }

            return Results.Ok();
        });
    }
}
