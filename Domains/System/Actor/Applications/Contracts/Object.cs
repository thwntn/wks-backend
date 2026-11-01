namespace Wks.Objects;

///
/// Summary:
///     Internal parameter objects for sub-account (actor) management use cases.
///
public class ActorObject
{
    #region Params
    public class CreateParams
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateParams
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class RoleParams
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public List<string> Permissions { get; set; } = [];
    }
    #endregion
}
