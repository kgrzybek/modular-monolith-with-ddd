using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingGroupPaymentRegisters.RegisterPayment
{
    public class RegisterPaymentCommand : CommandBase
    {
        public RegisterPaymentCommand(Guid meetingGroupId, DateTime startDate, DateTime endDate)
        {
            MeetingGroupId = meetingGroupId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Guid MeetingGroupId { get; }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }
    }
}