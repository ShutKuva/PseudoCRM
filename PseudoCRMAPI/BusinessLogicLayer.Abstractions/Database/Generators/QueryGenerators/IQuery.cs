namespace BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators
{
    public interface IQuery<T> : IQueryGenerator<T>
    {
        ICollection<IQueryGenerator<T>> QueryGenerators { get; }
    }
}