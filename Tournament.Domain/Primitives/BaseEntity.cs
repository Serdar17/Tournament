namespace Tournament.Domain.Primitives;

public abstract class BaseEntity
{
    public int Id { get; protected set; }

    protected BaseEntity(int id) => Id = id;

    protected BaseEntity() {}
}