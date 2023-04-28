using System.Linq.Expressions;
using BusinessLogicLayer.Abstractions.Database;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BusinessLogicLayer.Database.Services
{
    //public class PostgreHandler : IDatabaseService
    //{
    //    private readonly string _connectionString;
    //    private readonly NpgsqlConnection _connection;

    //    public PostgreHandler(string connectionString)
    //    {
    //        _connection = new NpgsqlConnection(connectionString);
    //    }

    //    public Task<IEnumerable<string>> GetCollections()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task AddEntity<T>(T entity, string collectionName = "")
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<IEnumerable<T>> GetEntities<T>(Expression<Func<T, bool>> predicate = null, string collectionName = "", int skip = 0, int take = 0, List<string> columns = null)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}