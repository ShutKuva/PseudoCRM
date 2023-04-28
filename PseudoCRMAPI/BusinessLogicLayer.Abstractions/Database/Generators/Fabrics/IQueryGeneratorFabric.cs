using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Database.Enums;

namespace BusinessLogicLayer.Abstractions.Database.Generators.Fabrics
{
    public interface IQueryGeneratorFabric
    {
        public IQueryGenerator GetGenerator(DatabaseProviders provider);
    }
}
