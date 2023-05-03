using BusinessLogicLayer.Abstractions.Database.Adapters;
using Core.Database;
using Core.Database.Dtos;
using DataAccessLayer.Abstractions;

namespace BusinessLogicLayer.Database.Adapters
{
    public class SqlServiceAdapter : IDatabaseServiceAdapter
    {
        private readonly IRepository<DatabaseObject> _databaseRepository;

        public SqlServiceAdapter(IRepository<DatabaseObject> databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public Task CreateDatabase(string connectionString, string databaseName = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DatabaseEntity>> GetEntities(
            DatabaseObject database,
            string collection,
            DatabasePredicateDto predicate = null,
            DatabaseColumnCollectionDto columns = null,
            DatabaseColumnCollectionDto groupBycolumns = null,
            DatabaseCollectionRelationCollectionDto relationCollection = null,
            int skip = 0,
            int take = 0)
        {
            DatabaseCollection? collectionObj = database.Collections.FirstOrDefault(collectionEnt => collectionEnt.Name == collection);

            if (collectionObj == null)
            {
                throw new ArgumentException("There is no collection with this name.");
            }

            return null;
        }

        private async Task<DatabasePredicate?> ConvertPredicateDto(DatabasePredicateDto? predicateDto)
        {
            if (predicateDto == null)
            {
                return null;
            }

            DatabasePredicate predicate = new DatabasePredicate();

            predicate.Data = predicateDto.Data;
            predicate.Operator = predicateDto.Operator;

            predicate.Column = await ConvertColumnDto(predicateDto.Column);
            predicate.Left = await ConvertPredicateDto(predicateDto.Left);
            predicate.Right = await ConvertPredicateDto(predicateDto.Right);

            return predicate;
        }

        private async Task<DatabaseColumn> ConvertColumnDto(DatabaseColumnDto? columnDto)
        {
            if (columnDto == null)
            {
                return null;
            }

            DatabaseColumn column = new DatabaseColumn();

            IEnumerable<DatabaseObject> columnsDatabase =
                await _databaseRepository.ReadByConditionAsync(database => database.Name == columnDto.DatabaseName, 0, 1);

            if (!columnsDatabase.Any())
            {
                throw new ArgumentException("There is no database with this name.");
            }

            return null;
        }
    }
}