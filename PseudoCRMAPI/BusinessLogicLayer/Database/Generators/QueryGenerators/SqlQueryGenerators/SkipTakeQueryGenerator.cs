using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using BusinessLogicLayer.PaginationTools;
using Core.Database;
using Core.Pagination;
using Microsoft.Extensions.Options;
using System.Text;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators
{
    public class SkipTakeQueryGenerator : IQueryGenerator<string>
    {
        private readonly DatabaseSkipTakePage _queryable;
        private readonly PaginationConfiguration _paginationConfiguration;

        public SkipTakeQueryGenerator(PaginationConfiguration paginationConfiguration, DatabaseSkipTakePage queryable)
        {
            _queryable = queryable;
            _paginationConfiguration = paginationConfiguration;
        }

        public string GetQuery()
        {
            (int Skip, int Take) processedSkipTake = Pagination.ProcessSkipTakeForPagination(_queryable.Skip, _queryable.Take, _queryable.Page,
                                                                    _paginationConfiguration.NumberOfEntitiesInOnePage);

            StringBuilder result = new StringBuilder();

            if (processedSkipTake.Skip > 0)
            {
                result.Append($"skip {processedSkipTake.Skip} ");
            }

            if (processedSkipTake.Take > 0)
            {
                result.Append($"take {processedSkipTake.Take} ");
            }

            return result.ToString();
        }
    }
}