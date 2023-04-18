namespace Tournament.Domain.Primitives;

public abstract class BaseEntity<T> 
{
    public T Id { get; set; }

    protected BaseEntity(T id) => Id = id;

    protected BaseEntity() {}
}