using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.AcceptMeetingGroupProposal
{
    internal class AcceptMeetingGroupProposalCommand : InternalCommandBase
    {
        public Guid MeetingGroupProposalId { get; }

        [JsonConstructor]
        internal AcceptMeetingGroupProposalCommand(Guid id, Guid meetingGroupProposalId) : base(id)
        {
            this.MeetingGroupProposalId = meetingGroupProposalId;
        }
    }
}