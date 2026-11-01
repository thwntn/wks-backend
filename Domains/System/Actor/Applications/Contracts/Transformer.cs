namespace Wks.DataTransformers;

///
/// Summary:
///     HTTP request contracts for sub-account (actor) management.
///     Covers creation, update, status change and role assignment for actors.
///
public class ActorDataTransformer
{
    public class Create
    {
        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public string Email { get; set; }

        [JsonRequired]
        public string Password { get; set; }

        public string? Description { get; set; }
    }

    public class Update
    {
        [JsonRequired]
        public string Name { get; set; }

        public string? Description { get; set; }
    }

    public class Role
    {
        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public string Code { get; set; }

        public string? Description { get; set; }

        [JsonRequired]
        public List<string> Permissions { get; set; } = [];
    }
}
