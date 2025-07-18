﻿namespace SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;

public record Media : ValueObject
{
    //TODO
    public static Media DefaultProfilePicture = new("url", "path.extension");

    [System.Text.Json.Serialization.JsonConstructor]
    [Newtonsoft.Json.JsonConstructor]
    private Media(string url, string filePath)
    {
        FilePath = filePath;
        Url = url;
        if (!string.IsNullOrEmpty(filePath))
        {
            MediaType = GetMediaType(FileExtension);
        }
    }

    private Media() { }

    public string Url { get; } = default!;
    public MediaType MediaType { get; }
    public string FilePath { get; } = default!;
    public string FileName => FilePath?.Split('/', '\\').LastOrDefault();
    public string FileExtension => FileName?.Split('.').LastOrDefault();

    public static Media Create(string url, string filePath = "path.extension")
    {
        return new Media(url, filePath);
    }

    private static MediaType GetMediaType(string fileExtension)
    {
        //TODO
        return MediaType.Image;
    }

    protected override IEnumerable<object> GetComponents()
    {
        yield return Url;
        yield return FilePath;
        yield return MediaType;
    }
}

public enum MediaType
{
    Image, Video, Audio
}

