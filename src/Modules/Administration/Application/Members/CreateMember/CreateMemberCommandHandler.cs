using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Administration.Domain.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Members
{
    internal class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, Guid>
    {
        private readonly IMemberRepository _memberRepository;

        public CreateMemberCommandHandler(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Guid> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = Member.Create(
                request.MemberId,
                request.Login,
                request.Email,
                request.FirstName,
                request.LastName,
                request.Name);

            await _memberRepository.AddAsync(member);

            return member.Id.Value;
        }
    }
}