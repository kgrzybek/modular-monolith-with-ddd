using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Members.CreateMember
{
    public class MemberCratedNotificationHandler : INotificationHandler<MemberCreatedNotification>
    {
        public Task Handle(MemberCreatedNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}