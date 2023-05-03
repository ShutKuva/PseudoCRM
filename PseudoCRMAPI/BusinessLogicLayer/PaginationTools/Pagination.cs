namespace BusinessLogicLayer.PaginationTools
{
    public static class Pagination
    {
        public static (int Skip, int Take) ProcessSkipTakeForPagination(int skip, int take, int page, int numberOfEntities)
        {
            if (take < numberOfEntities)
            {
                return (skip, take);
            } 
            else if (take < page * numberOfEntities)
            {
                throw new ArgumentException("To many pages");
            }

            return (skip + numberOfEntities * page, take - numberOfEntities * page);
        }
    }
}