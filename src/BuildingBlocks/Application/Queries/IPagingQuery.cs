namespace CompanyName.MyMeetings.BuildingBlocks.Application.Queries
{
    public interface IPagedQuery
    {
        /// <summary>
        /// Page number. If null then default is 1.
        /// </summary>
        int? Page { get; }

        /// <summary>
        /// Records number per page (page size).
        /// </summary>
        int? PerPage { get; }
    }
}