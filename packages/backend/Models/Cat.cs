namespace CatMashApi.Models;

public class Cat
{
    public Guid Id { get; set; }
    public int Sequence { get; set; }
    public string ExternalId { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public int LikeCount { get; set; }
}