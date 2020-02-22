using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.CreatePaymentRegister
{
    public class CreatePaymentRegisterCommand : InternalCommandBase<Guid>
    {
        [JsonConstructor]
        public CreatePaymentRegisterCommand(Guid id, Guid meetingGroupProposalId) : base(id)
        {
            this.MeetingGroupProposalId = meetingGroupProposalId;
        }

        internal Guid MeetingGroupProposalId { get; }
    }
}