namespace Wks.Objects;

///
/// Summary:
///     Request parameter objects for version management operations.
///
public class VersionObject
{
    public class CreateParams
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool Reset { get; set; }
        public bool Highlight { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateParams
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool Reset { get; set; }
        public bool Highlight { get; set; }
        public string? Description { get; set; }
    }

    public class CreateFeatureParams
    {
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
