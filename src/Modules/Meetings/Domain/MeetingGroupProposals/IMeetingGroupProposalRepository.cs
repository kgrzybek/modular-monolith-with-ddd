namespace CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals
{
    public interface IMeetingGroupProposalRepository
    {
        Task AddAsync(MeetingGroupProposal meetingGroupProposal);

        Task<MeetingGroupProposal> GetByIdAsync(MeetingGroupProposalId meetingGroupProposalId);
    }
}