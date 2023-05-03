using Core.Abstractions.Database;
using Core.Database;

namespace BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators
{
    public interface IQueryGenerator<T>
    {
        public T GetQuery();
    }
}
