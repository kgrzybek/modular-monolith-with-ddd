using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Domain.MeetingFeePayments
{
    public class MeetingFeePaymentStatus : ValueObject
    {
        public static MeetingFeePaymentStatus WaitingForPayment => new MeetingFeePaymentStatus(nameof(WaitingForPayment));

        public static MeetingFeePaymentStatus Paid => new MeetingFeePaymentStatus(nameof(Paid));

        public static MeetingFeePaymentStatus Expired => new MeetingFeePaymentStatus(nameof(Expired));

        public string Code { get; }

        private MeetingFeePaymentStatus(string code)
        {
            Code = code;
        }

        public static MeetingFeePaymentStatus Of(string code)
        {
            return new MeetingFeePaymentStatus(code);
        }
    }
}