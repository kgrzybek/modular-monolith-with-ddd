using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.CreatePaymentRegister
{
    internal class CreatePaymentRegisterCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal CreatePaymentRegisterCommand(Guid id, Guid meetingGroupProposalId) : base(id)
        {
            this.MeetingGroupProposalId = meetingGroupProposalId;
        }

        internal Guid MeetingGroupProposalId { get; }
    }
}