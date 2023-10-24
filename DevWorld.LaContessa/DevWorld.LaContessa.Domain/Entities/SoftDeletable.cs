namespace DevWorld.LaContessa.Domain.Entities;

public class SoftDeletable : BaseEntity, ISoftDeletable
{
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    protected SoftDeletable(Guid id) : base(id) { }
    protected SoftDeletable() { }
}

