namespace Wks.Interfaces;

public interface IListRoleActor
{
    Task<List<Role>> ExecuteAsync(Guid profileId, Guid actorId);
}
