using Core.Abstractions.Database;

namespace Core.Database
{
    public class DatabaseSkipTakePage : IDatabaseQueryable
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Page { get; set; }

        public DatabaseSkipTakePage(int skip = 0, int take = 0, int page = 0)
        {
            Skip = skip;
            Take = take;
            Page = page;
        }
    }
}