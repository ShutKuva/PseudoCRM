using System.Text;
using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;

namespace BusinessLogicLayer.Database.Providers.Sql
{
    public class SqlQuery : IQuery<string>
    {
        public ICollection<IQueryGenerator<string>> QueryGenerators { get; } = new List<IQueryGenerator<string>>();

        public string GetQuery()
        {
            StringBuilder result = new StringBuilder();

            foreach (IQueryGenerator<string> generator in QueryGenerators)
            {
                result.Append(generator.GetQuery() + " ");
            }

            return result.ToString();
        }
    }
}