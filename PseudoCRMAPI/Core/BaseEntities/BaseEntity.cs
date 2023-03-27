namespace Core.BaseEntities
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; } = default!;
    }
}