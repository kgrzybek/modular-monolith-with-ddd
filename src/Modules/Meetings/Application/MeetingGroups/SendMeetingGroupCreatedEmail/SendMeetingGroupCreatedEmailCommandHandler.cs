using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members;
using Dapper;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.SendMeetingGroupCreatedEmail
{
    internal class SendMeetingGroupCreatedEmailCommandHandler : ICommandHandler<SendMeetingGroupCreatedEmailCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IEmailSender _emailSender;

        public SendMeetingGroupCreatedEmailCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory, 
            IEmailSender emailSender)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(SendMeetingGroupCreatedEmailCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var meetingGroup = await connection.QuerySingleAsync<MeetingGroupDto>("SELECT " +
                                  "[MeetingGroup].[Name], " +
                                  "[MeetingGroup].[LocationCountryCode], " +
                                  "[MeetingGroup].[LocationCity] " +
                                  "FROM [meetings].[v_MeetingGroups] AS [MeetingGroup] " +
                                  "WHERE [MeetingGroup].[Id] = @Id", new
            {
                Id = request.MeetingGroupId.Value
            });

            var member = await MembersQueryHelper.GetMember(request.CreatorId, connection);

            var email = new EmailMessage(
                member.Email, 
                $"{meetingGroup.Name} created",
                $"{meetingGroup.Name} created at {meetingGroup.LocationCity}, {meetingGroup.LocationCountryCode}");

            _emailSender.SendEmail(email);

            return Unit.Value;
        }
    }
}