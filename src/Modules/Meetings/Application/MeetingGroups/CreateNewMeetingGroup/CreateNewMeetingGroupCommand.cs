using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Processing.InternalCommands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.CreateNewMeetingGroup
{
    internal class CreateNewMeetingGroupCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal CreateNewMeetingGroupCommand(Guid id, MeetingGroupProposalId meetingGroupProposalId) : base(id)
        {
            this.MeetingGroupProposalId = meetingGroupProposalId;
        }

        internal MeetingGroupProposalId MeetingGroupProposalId { get;  }
    }
}