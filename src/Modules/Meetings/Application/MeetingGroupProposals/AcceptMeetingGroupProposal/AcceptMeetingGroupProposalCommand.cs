using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    internal class AcceptMeetingGroupProposalCommand : CommandBase
    {
        public Guid MeetingGroupProposalId { get; }
        public AcceptMeetingGroupProposalCommand(Guid meetingGroupProposalId)
        {
            MeetingGroupProposalId = meetingGroupProposalId;
        }

        [JsonConstructor]
        public AcceptMeetingGroupProposalCommand(Guid id, Guid meetingGroupProposalId) : base(id)
        {
            this.MeetingGroupProposalId = meetingGroupProposalId;
        }
    }
}