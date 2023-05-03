using System.Text;
using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Abstractions.Database;
using Core.Database;

namespace BusinessLogicLayer.Database.Generators.QueryGenerators.SqlQueryGenerators
{
    public class JoinQueryGenerator : IQueryGenerator<string>
    {
        private readonly DatabaseCollectionRelationCollection _queryable;

        public JoinQueryGenerator(DatabaseCollectionRelationCollection queryable)
        {
            _queryable = queryable;
        }

        public string GetQuery()
        {
            StringBuilder result = new StringBuilder();

            foreach (DatabaseCollectionRelation relation in _queryable.Relations)
            {
                result.Append(ProcessRelation(relation) + " ");
            }

            return result.ToString();
        }

        private string ProcessRelation(DatabaseCollectionRelation relation)
        {
            return $"inner join [{relation.SecondCollection.Name}] on [{relation.SecondCollection.Name}].[{relation.SecondCollection.PrimaryColumn.Name}] = [{relation.FirstCollection.Name}].[{relation.FirstCollection.Columns.First(c => c.ForeignCollection == relation.SecondCollection).Name}]";
        }
    }
}