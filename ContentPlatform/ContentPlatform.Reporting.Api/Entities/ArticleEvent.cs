namespace ContentPlatform.Reporting.Api.Entities;

public class ArticleEvent
{
    public Guid Id { get; set; }

    public Guid ArticleId { get; set; }

    public ArticleEventType EventType { get; set; }

    public DateTime CreatedOnUtc { get; set; }
}