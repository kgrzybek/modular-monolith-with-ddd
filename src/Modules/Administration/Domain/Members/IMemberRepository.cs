namespace CompanyName.MyMeetings.Modules.Administration.Domain.Members
{
    /// <summary>
    /// Represents a repository for managing members.
    /// </summary>
    public interface IMemberRepository
    {
        /// <summary>
        /// Adds a new member asynchronously.
        /// </summary>
        /// <param name="member">The member to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Member member);

        /// <summary>
        /// Retrieves a member by its ID asynchronously.
        /// </summary>
        /// <param name="memberId">The ID of the member to retrieve.</param>
        /// <returns>A task representing the asynchronous operation and containing the retrieved member.</returns>
        Task<Member> GetByIdAsync(MemberId memberId);
    }
}