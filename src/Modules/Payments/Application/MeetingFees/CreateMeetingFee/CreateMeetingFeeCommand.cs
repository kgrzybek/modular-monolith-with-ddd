using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.CreateMeetingFee
{
    public class CreateMeetingFeeCommand : InternalCommandBase<Guid>
    {
        [JsonConstructor]
        public CreateMeetingFeeCommand(
            Guid id, Guid payerId, Guid meetingId, decimal value, string currency)
            : base(id)
        {
            PayerId = payerId;
            MeetingId = meetingId;
            Value = value;
            Currency = currency;
        }

        internal decimal Value { get; }

        internal string Currency { get; }

        internal Guid PayerId { get; }

        internal Guid MeetingId { get; }
    }
}