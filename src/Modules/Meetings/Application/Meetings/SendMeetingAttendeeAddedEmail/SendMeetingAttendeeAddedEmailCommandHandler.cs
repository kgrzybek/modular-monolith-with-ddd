using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Emails;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Meetings.Application.Members;

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

        public async Task Handle(SendMeetingAttendeeAddedEmailCommand request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            var member = await MembersQueryHelper.GetMember(request.AttendeeId, connection);

            var meeting = await MeetingsQueryHelper.GetMeeting(request.MeetingId, connection);

            var email = new EmailMessage(
                member.Email,
                $"You joined to {meeting.Title} meeting.",
                $"You joined to {meeting.Title} title at {meeting.TermStartDate.ToShortDateString()} - {meeting.TermEndDate.ToShortDateString()}, location {meeting.LocationAddress}, {meeting.LocationPostalCode}, {meeting.LocationCity}");

            await _emailSender.SendEmail(email);
        }
    }
}