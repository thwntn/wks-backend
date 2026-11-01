namespace Wks.DataTransformers;

///
/// Summary:
///     DTOs for version management API requests.
///
public class VersionDataTransformer
{
    public class Create
    {
        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public string Version { get; set; }

        public bool Reset { get; set; } = false;

        public bool Highlight { get; set; } = false;

        public string? Description { get; set; }
    }

    public class Update
    {
        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public string Version { get; set; }

        public bool Reset { get; set; } = false;

        public bool Highlight { get; set; } = false;

        public string? Description { get; set; }
    }

    public class CreateFeature
    {
        [JsonRequired]
        public string Title { get; set; }

        public string? Description { get; set; }
    }
}
