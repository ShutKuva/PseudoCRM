using System.Text;
using Core.BaseEntities;

namespace Core.Database.Connections.BaseEntities
{
    public class DatabaseConnection : BaseEntity
    {
        public List<(string Property, string Value)> Properties;
        public DatabaseConnection() { }

        public string GetConnectionString()
        {
            StringBuilder sb = new StringBuilder();

            foreach ((string, string) property in Properties)
            {
                sb.Append($"{property.Item1}={property.Item2};");
            }

            return sb.ToString();
        }
    }
}