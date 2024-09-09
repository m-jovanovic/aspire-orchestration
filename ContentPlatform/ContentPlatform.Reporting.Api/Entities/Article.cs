namespace ContentPlatform.Reporting.Api.Entities;

public class Article
{
    public Guid Id { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? PublishedOnUtc { get; set; }
}