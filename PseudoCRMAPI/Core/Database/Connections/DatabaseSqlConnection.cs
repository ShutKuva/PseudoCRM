using Core.Database.Connections.BaseEntities;

namespace Core.Database.Connections
{
    public class DatabaseSqlConnection : DatabaseConnection
    {
        public string Database { get; set; }
        public string Server { get; set; }
        public string TrustedConnection { get; set; }

        public DatabaseSqlConnection()
        {
            
        }

        public DatabaseSqlConnection(string connectionString)
        {
            List<string[]> listWithParams = connectionString.Split(";").Select(keyValue => keyValue.Split("=").ToArray()).ToList();

            foreach (string[] keyValue in listWithParams)
            {
                switch (keyValue[0])
                {
                    case "Server":
                        Server = keyValue[1];
                        break;
                    case "Database":
                        Database = keyValue[1];
                        break;
                    case "Trusted_Connection":
                        TrustedConnection = keyValue[1];
                        break;
                }
            }
        }

        //public override string GetConnectionString()
        //{
        //    return $"Server={Server};Database={Database};Trusted_Connection={TrustedConnection}";
        //}
    }
}