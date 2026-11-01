namespace Wks.Objects;

public class FeatureObject
{
    public class Extension(
        Guid id,
        string name,
        string description,
        string icon,
        List<string> permission
    )
    {
        public Guid Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public string Icon { get; set; } = icon;
        public string Enum { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Permission { get; set; } = permission;
    }
}
