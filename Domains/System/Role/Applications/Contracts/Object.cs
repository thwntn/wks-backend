namespace Wks.Objects;

///
/// Summary:
///     Request parameter objects for role management operations.
///
public class RoleObject
{
    public class CreateParams
    {
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public List<string> Permissions { get; set; } = [];
    }

    public class UpdateParams
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        /// <summary>List of PerrmissionContant enum codes.</summary>
        public List<string> Permissions { get; set; } = [];
    }

    public class RemoveParams
    {
        public List<Guid> Ids { get; set; }
    }

    public class AssignParams
    {
        public Guid AccountId { get; set; }
    }

    public class UnassignParams
    {
        public Guid AccountId { get; set; }
    }

    /// <summary>
    ///     Master permission definition read from Permission.JSON metadata.
    /// </summary>
    public class PermissionMaster
    {
        public string Enum { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
    }
}
