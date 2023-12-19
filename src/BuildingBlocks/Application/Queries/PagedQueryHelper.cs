namespace CompanyName.MyMeetings.BuildingBlocks.Application.Queries
{
    /// <summary>
    /// Helper class for working with paged queries.
    /// </summary>
    public static class PagedQueryHelper
    {
        public const string Offset = "Offset";

        public const string Next = "Next";

        /// <summary>
        /// Gets the page data based on the provided paged query, or default values if the query is null,
        /// or the page or per page values are null.
        /// </summary>
        /// <param name="query">The paged query.</param>
        /// <returns>The page data.</returns>
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

        /// <summary>
        /// Appends the page statement to the SQL query.
        /// </summary>
        /// <param name="sql">The SQL query.</param>
        /// <returns>The updated SQL query with the page statement.</returns>
        public static string AppendPageStatement(string sql)
        {
            return $@"{sql}
            OFFSET @{Offset} ROWS FETCH NEXT @{Next} ROWS ONLY;";
        }
    }
}