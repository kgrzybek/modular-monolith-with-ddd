namespace CompanyName.MyMeetings.BuildingBlocks.Application.Queries
{
    public static class PagedQueryHelper
    {
        public const string Offset = "Offset";

        public const string Next = "Next";

        public static PageData GetPageData(IPagedQuery query)
        {
            int offset;
            if (!query.Page.HasValue ||
                !query.PerPage.HasValue)
            {
                offset = 0;
            }
            else
            {
                offset = (query.Page.Value - 1) * query.PerPage.Value;
            }

            int next;
            if (!query.PerPage.HasValue)
            {
                next = int.MaxValue;
            }
            else
            {
                next = query.PerPage.Value;
            }

            return new PageData(offset, next);
        }

        public static string AppendPageStatement(string sql)
        {
            return $"{sql} " +
                   $"OFFSET @{Offset} ROWS FETCH NEXT @{Next} ROWS ONLY; ";
        }
    }
}