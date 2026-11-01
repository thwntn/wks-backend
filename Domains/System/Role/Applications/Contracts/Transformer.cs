namespace Wks.DataTransformers;

///
/// Summary:
///     DTOs for role management API requests.
///
public class RoleDataTransformer
{
    public class Create
    {
        [JsonRequired]
        public Guid AccountId { get; set; }

        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public string Code { get; set; }

        public string? Description { get; set; }

        [JsonRequired]
        public List<string> Permissions { get; set; } = [];
    }

    public class Update
    {
        [JsonRequired]
        public string Name { get; set; }

        public string? Description { get; set; }

        [JsonRequired]
        public List<string> Permissions { get; set; } = [];
    }

    public class Remove
    {
        [JsonRequired]
        public List<Guid> Ids { get; set; }
    }

    public class Assign
    {
        [JsonRequired]
        public Guid AccountId { get; set; }
    }

    public class Unassign
    {
        [JsonRequired]
        public Guid AccountId { get; set; }
    }

    public class MakeAdmin
    {
        [JsonRequired]
        public Guid AccountId { get; set; }
    }
}
