public class CatData
{
    public List<ImageData> Images { get; set; } = null!;
}

public class ImageData
{
    public string url { get; set; } = null!;
    public string id { get; set; } = null!;
}