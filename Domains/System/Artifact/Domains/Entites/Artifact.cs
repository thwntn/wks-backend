namespace Wks.Databases;

public class Artifact(
    string objectKey,
    string rawName,
    string token,
    string objectName,
    string fileType,
    string extension
) : Entity, IEntity
{
    [Required]
    public string ObjectKey { get; set; } = objectKey;

    [Required]
    public string RawName { get; set; } = rawName;

    [Required]
    public string Token { get; set; } = token;

    [Required]
    public string ObjectName { get; set; } = objectName;

    [Required]
    public string FileType { get; set; } = fileType;

    [Required]
    public string Extension { get; set; } = extension;

    [NotMapped]
    public string Url
    {
        get => Reader.CreateURL(Token);
    }
}
