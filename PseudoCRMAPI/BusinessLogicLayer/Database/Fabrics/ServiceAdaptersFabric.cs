using BusinessLogicLayer.Abstractions.Database;
using BusinessLogicLayer.Abstractions.Database.Adapters;
using BusinessLogicLayer.Abstractions.Fabrics;
using Core.Database;
using Core.Database.Dtos;
using DataAccessLayer.Abstractions;

namespace BusinessLogicLayer.Database.Fabrics
{
    public class ServiceAdaptersFabric : IFabric<string, IDatabaseServiceAdapter>
    {
        private readonly IRepository<DatabaseObject> _databaseRepository;

        public ServiceAdaptersFabric(IRepository<DatabaseObject> databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public IDatabaseServiceAdapter GetGenerator(string provider)
        {
            throw new NotImplementedException();
        }
    }
}