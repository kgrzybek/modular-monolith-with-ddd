namespace CompanyName.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals
{
    public interface IMeetingGroupProposalRepository
    {
        Task AddAsync(MeetingGroupProposal meetingGroupProposal);

        Task<MeetingGroupProposal> GetByIdAsync(MeetingGroupProposalId meetingGroupProposalId);
    }
}