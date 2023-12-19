using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Administration.Domain.Members;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Members.CreateMember
{
    /// <summary>
    /// Handles the command to create a new member.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateMemberCommandHandler"/> class.
    /// </remarks>
    /// <param name="memberRepository">The member repository.</param>
    internal class CreateMemberCommandHandler(IMemberRepository memberRepository) : ICommandHandler<CreateMemberCommand, Guid>
    {
        private readonly IMemberRepository _memberRepository = memberRepository;

        /// <summary>
        /// Handles the create member command and returns the ID of the newly created member.
        /// </summary>
        /// <param name="request">The create member command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The ID of the newly created member.</returns>
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