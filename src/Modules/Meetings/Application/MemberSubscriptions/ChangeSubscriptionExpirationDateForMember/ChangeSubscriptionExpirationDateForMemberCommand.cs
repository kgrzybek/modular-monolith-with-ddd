using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions.ChangeSubscriptionExpirationDateForMember
{
    public class ChangeSubscriptionExpirationDateForMemberCommand : InternalCommandBase
    {
        [JsonConstructor]
        public ChangeSubscriptionExpirationDateForMemberCommand(
            Guid id,
            MemberId memberId,
            DateTime expirationDate)
            : base(id)
        {
            MemberId = memberId;
            ExpirationDate = expirationDate;
        }

        public MemberId MemberId { get; }

        public DateTime ExpirationDate { get; }
    }
}