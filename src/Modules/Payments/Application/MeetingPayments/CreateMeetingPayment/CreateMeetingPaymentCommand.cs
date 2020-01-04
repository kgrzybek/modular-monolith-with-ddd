using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.MeetingPayments;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingPayments.CreateMeetingPayment
{
    internal class CreateMeetingPaymentCommand : InternalCommandBase
    {
        [JsonConstructor]
        internal CreateMeetingPaymentCommand(Guid id, PayerId payerId, MeetingId meetingId, decimal value, string currency) : base(id)
        {
            PayerId = payerId;
            MeetingId = meetingId;
            Value = value;
            Currency = currency;
        }

        internal decimal Value { get; }

        internal string Currency { get; }

        internal PayerId PayerId { get; }

        internal MeetingId MeetingId { get; }
    }
}