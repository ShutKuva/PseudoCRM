using BusinessLogicLayer.Abstractions.Database;
using Core.Database;
using Dapper;
using System.Data.SqlClient;
using Core.Database.Connections;
using Core.Database.Enums;
using DataAccessLayer.Abstractions;

namespace BusinessLogicLayer.Database.Services
{
    public class SqlServerServices : IDatabaseService
    {
        private string? _databaseName;
        private readonly IRepository<DatabaseObject> _databaseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SqlServerServices(IRepository<DatabaseObject> databaseRepository, IUnitOfWork unitOfWork)
        {
        }

        public async Task CreateDatabase(string connectionString, string databaseName = null)
        {
            DatabaseObject databaseObject = new DatabaseObject();

            DatabaseSqlConnection connection = new DatabaseSqlConnection(connectionString);

            databaseObject.Connection = connection;
            databaseObject.Collections = await GetCollections(connectionString);
            databaseObject.Name = databaseName != null ? databaseName : connection.Database;

            await ConfigureForeignKeys(connectionString, databaseObject);

            await _databaseRepository.CreateAsync(databaseObject);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<IEnumerable<T>> GetEntities<T>(DatabasePredicate predicate = null,
            string collectionName = "",
            int skip = 0,
            int take = 0,
            List<DatabaseColumn> columns = null)
        {
            return null;
        }

        public Task AddEntity<T>(T entity, string collectionName = "")
        {
            throw new NotImplementedException();
        }

        private async Task<List<DatabaseCollection>> GetCollections(string connectionString)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            List<DatabaseCollection> result = new List<DatabaseCollection>();

            IEnumerable<string> collections = await connection.QueryAsync<string>("select TABLE_NAME from INFORMATION_SCHEMA.TABLES");

            foreach (string collectionName in collections)
            {
                result.Add(await GetCollection(connection, collectionName));
            }

            return null;
        }

        private async Task<DatabaseCollection> GetCollection(SqlConnection connection, string collectionName)
        {
            DatabaseCollection collection = new DatabaseCollection();

            collection.Name = collectionName;
            collection.Columns = await GetColumns(connection, collection);
            collection.PrimaryColumn = await GetPrimaryColumn(connection, collection);

            return collection;
        }

        private async Task<List<DatabaseColumn>> GetColumns(SqlConnection connection, DatabaseCollection collection)
        {
            List<DatabaseColumn> result = new List<DatabaseColumn>();

            IEnumerable<(string, int, int)> columnNames = await connection.QueryAsync<(string, int, int)>($@"select [cl].[name], [cl].[column_id], [cl].[system_type_id]
                                                                            from sys.all_columns cl
                                                                            inner join sys.tables tb
                                                                            on [tb].[name] = '{collection.Name}' and [tb].[object_id] = [cl].[object_id]");
            foreach ((string, int, int) str in columnNames)
            {
                result.Add(new DatabaseColumn()
                {
                    ColumnId = str.Item2,
                    Name = str.Item1,
                    Type = (DatabaseTypes)str.Item3,
                });
            }

            return result;
        }

        private async Task ConfigureForeignKeys(string connectionString, DatabaseObject database)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            foreach (DatabaseCollection collection in database.Collections)
            {
                IEnumerable<(string, string)> allForeignColumns = await connection.QueryAsync<(string, string)>($@"select [acl].[name], [tbr].[name]
                                                    from sys.foreign_key_columns cl
                                                    inner join sys.tables tbp
                                                    on [tbp].[object_id] = [cl].[parent_object_id] and [tbp].[name] = '{collection.Name}'
                                                    inner join  sys.tables tbr
                                                    on [tbr].[object_id] = [cl].[referenced_object_id]
                                                    inner join sys.all_columns acl
                                                    on [acl].[object_id] = [tbp].[object_id] and [acl].[column_id] = [cl].[parent_column_id] ");
                
                foreach ((string, string) foreignColumn in allForeignColumns)
                {
                    collection.Columns.FirstOrDefault(c => c.Name == foreignColumn.Item1).ForeignCollection =
                        database.Collections.FirstOrDefault(col => col.Name == foreignColumn.Item2);
                }
            }
        }

        private async Task<List<DatabaseColumn>> GetForeignColumns(SqlConnection connection, DatabaseCollection collection, DatabaseCollection referenceCollection = null)
        {
            List<DatabaseColumn> result = new List<DatabaseColumn>();

            IEnumerable<string> foreignColumnsNames = await connection.QueryAsync<string>(@$"select [cl].[name]
                                                        from sys.foreign_key_columns fk
                                                        inner join sys.tables tbp
                                                        on [tbp].[object_id] = [fk].[parent_object_id] and [tbp].[name] = '{collection.Name}'
                                                        inner join sys.tables tbr
                                                        on [tbr].[object_id] = [fk].[referenced_object_id] {(referenceCollection == null ? "" : $"and [tbr].[name] = '{referenceCollection.Name}'")}
                                                        inner join sys.columns cl
                                                        on [fk].[parent_object_id] = [cl].[object_id] and [fk].[parent_column_id] = [cl].[column_id]");

            foreach (string column in foreignColumnsNames)
            {
                //result.Add(collection.ForeignColumns.FirstOrDefault(c => c.Name == column));
            }

            return result;
        }

        private async Task<DatabaseColumn> GetPrimaryColumn(SqlConnection connection, DatabaseCollection collection)
        {
            DatabaseColumn result;

            string primaryColumnName = await connection.ExecuteScalarAsync<string>(@$"select [kc].[COLUMN_NAME]
                                                                                        from INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
                                                                                        inner join INFORMATION_SCHEMA.KEY_COLUMN_USAGE kc
                                                                                        on [tc].CONSTRAINT_NAME = [kc].[CONSTRAINT_NAME]
                                                                                        where [tc].[CONSTRAINT_TYPE] = 'PRIMARY KEY' and [tc].[TABLE_NAME] = '{collection.Name}'");

            return collection.Columns.FirstOrDefault(c => c.Name == primaryColumnName);
        }
    }
}