namespace BusinessLogicLayer.Abstractions.Services
{
    public interface ICreateService<TEntity>
    {
        Task CreateAsync(TEntity entity);
    }
}