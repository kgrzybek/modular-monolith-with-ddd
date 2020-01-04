using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.Emails;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeAddedEmail
{
    internal class SendMeetingAttendeeAddedEmailCommandHandler : ICommandHandler<SendMeetingAttendeeAddedEmailCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IEmailSender _emailSender;

        internal SendMeetingAttendeeAddedEmailCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory, 
            IEmailSender emailSender)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(SendMeetingAttendeeAddedEmailCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var member = await MembersQueryHelper.GetMember(request.AttendeeId, connection);

            var meeting = await MeetingsQueryHelper.GetMeeting(request.MeetingId, connection);

            var email = new EmailMessage(member.Email, $"You joined to {meeting.Title} meeting.",
                $"You joined to {meeting.Title} title at {meeting.TermStartDate.ToShortDateString()} - {meeting.TermEndDate.ToShortDateString()}, location {meeting.LocationAddress}, {meeting.LocationPostalCode}, {meeting.LocationCity}");

            _emailSender.SendEmail(email);

            return Unit.Value;
        }
    }
}