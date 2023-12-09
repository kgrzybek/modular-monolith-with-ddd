using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.MeetingFees.GetMeetingFees
{
    public class GetMeetingFeesQuery : QueryBase<List<MeetingFeeDto>>
    {
        public GetMeetingFeesQuery(Guid meetingId)
        {
            MeetingId = meetingId;
        }

        public Guid MeetingId { get; }
    }
}