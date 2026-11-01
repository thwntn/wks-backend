namespace Wks.Databases;

using Wks.Interfaces;

public class Cash(string name, long quantity) : Entity, IEntity
{
    [Required]
    public string Name { get; set; } = name;

    [Required]
    public bool IsActive { get; set; } = true;

    public long TotalAbstract;

    public long TotalSubtract;

    [Required]
    public long Quantity { get; set; } = quantity;

    [Required]
    public Guid ProfileId { get; set; }

    [ForeignKey(nameof(ProfileId))]
    public Profile Profile { get; set; }

    public ICollection<CashDescription> CashDescriptions { get; set; }
}
