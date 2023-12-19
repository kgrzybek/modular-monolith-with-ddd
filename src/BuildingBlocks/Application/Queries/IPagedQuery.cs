namespace CompanyName.MyMeetings.BuildingBlocks.Application.Queries
{
    /// <summary>
    /// Represents a paged query.
    /// </summary>
    public interface IPagedQuery
    {
        /// <summary>
        /// Gets or sets the page number. If null, the default is 1.
        /// </summary>
        int? Page { get; }

        /// <summary>
        /// Gets or sets the number of records per page (page size).
        /// </summary>
        int? PerPage { get; }
    }
}