using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.BuildingBlocks.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members;
using Dapper;

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

        public async Task Handle(SendMeetingGroupCreatedEmailCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var meetingGroup = await connection.QuerySingleAsync<MeetingGroupDto>(
                "SELECT " +
                                  "[MeetingGroup].[Name], " +
                                  "[MeetingGroup].[LocationCountryCode], " +
                                  "[MeetingGroup].[LocationCity] " +
                                  "FROM [meetings].[v_MeetingGroups] AS [MeetingGroup] " +
                                  "WHERE [MeetingGroup].[Id] = @Id", 
                new
                                  {
                                      Id = request.MeetingGroupId.Value
                                  });

            var member = await MembersQueryHelper.GetMember(request.CreatorId, connection);

            var email = new EmailMessage(
                member.Email,
                $"{meetingGroup.Name} created",
                $"{meetingGroup.Name} created at {meetingGroup.LocationCity}, {meetingGroup.LocationCountryCode}");

            await _emailSender.SendEmail(email);
        }
    }
}