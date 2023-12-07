namespace DevWorld.LaContessa.Domain.Entities;

public class SoftDeletable : BaseEntity, ISoftDeletable
{
    protected SoftDeletable(Guid id) : base(id)
    {
    }

    protected SoftDeletable()
    {
    }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}

