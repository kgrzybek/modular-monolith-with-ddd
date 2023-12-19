namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    /// <summary>
    /// Represents a repository for managing meeting group proposals.
    /// </summary>
    public interface IMeetingGroupProposalRepository
    {
        /// <summary>
        /// Adds a new meeting group proposal to the repository.
        /// </summary>
        /// <param name="meetingGroupProposal">The meeting group proposal to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(MeetingGroupProposal meetingGroupProposal);

        /// <summary>
        /// Retrieves a meeting group proposal by its ID from the repository.
        /// </summary>
        /// <param name="meetingGroupProposalId">The ID of the meeting group proposal to retrieve.</param>
        /// <returns>A task representing the asynchronous operation and containing the retrieved meeting group proposal.</returns>
        Task<MeetingGroupProposal> GetByIdAsync(MeetingGroupProposalId meetingGroupProposalId);
    }
}