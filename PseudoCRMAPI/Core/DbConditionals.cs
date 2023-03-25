namespace Core
{
    public abstract class DbCredentials
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? ConnectionString { get; set; }

        public abstract IEnumerable<string> GetAllTables();
    }
}