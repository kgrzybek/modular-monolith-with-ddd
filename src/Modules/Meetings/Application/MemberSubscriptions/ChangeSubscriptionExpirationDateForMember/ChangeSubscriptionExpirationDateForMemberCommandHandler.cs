using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Domain.Members.MemberSubscriptions;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MemberSubscriptions.ChangeSubscriptionExpirationDateForMember
{
    public class ChangeSubscriptionExpirationDateForMemberCommandHandler : ICommandHandler<ChangeSubscriptionExpirationDateForMemberCommand>
    {
        private readonly IMemberSubscriptionRepository _memberSubscriptionRepository;

        public ChangeSubscriptionExpirationDateForMemberCommandHandler(IMemberSubscriptionRepository memberSubscriptionRepository)
        {
            _memberSubscriptionRepository = memberSubscriptionRepository;
        }

        public async Task<Unit> Handle(ChangeSubscriptionExpirationDateForMemberCommand command, CancellationToken cancellationToken)
        {
            MemberSubscription memberSubscription = await _memberSubscriptionRepository.GetByIdOptionalAsync(new MemberSubscriptionId(command.MemberId.Value));

            if (memberSubscription == null)
            {
                memberSubscription = MemberSubscription.CreateForMember(command.MemberId, command.ExpirationDate);
                await _memberSubscriptionRepository.AddAsync(memberSubscription);
            }
            else
            {
                memberSubscription.ChangeExpirationDate(command.ExpirationDate);
            }

            return Unit.Value;
        }
    }
}