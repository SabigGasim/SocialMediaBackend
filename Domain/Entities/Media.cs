using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Entities;

public record Media : ValueObject
{
    //TODO
    public static Media DefaultProfilePicture = new("", "");

    public Media(string url, string filePath)
    {
        FilePath = filePath;
        Url = url;
        MediaType = GetMediaType(FileExtension);
    }

    private Media() { }

    public string Url { get; }
    public MediaType MediaType { get; }
    public string FilePath { get; }
    public string FileName => FilePath.Split('/','\\').Last();
    public string FileExtension => FileName.Split('.').Last();

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

