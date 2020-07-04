using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFees
{
    public class MeetingFeeStatus : ValueObject
    {
        public static MeetingFeeStatus WaitingForPayment => new MeetingFeeStatus(nameof(WaitingForPayment));

        public static MeetingFeeStatus Paid => new MeetingFeeStatus(nameof(Paid));

        public static MeetingFeeStatus Expired => new MeetingFeeStatus(nameof(Expired));

        public static MeetingFeeStatus Canceled => new MeetingFeeStatus(nameof(Canceled));

        public string Code { get; }

        private MeetingFeeStatus(string code)
        {
            Code = code;
        }

        public static MeetingFeeStatus Of(string code)
        {
            return new MeetingFeeStatus(code);
        }
    }
}