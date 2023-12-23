using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using Dapper;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments
{
    internal class MeetingCommentLikedNotificationHandler : INotificationHandler<MeetingCommentLikedNotification>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public MeetingCommentLikedNotificationHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task Handle(MeetingCommentLikedNotification notification, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = """
                               UPDATE [meetings].[MeetingComments] 
                               SET [LikesCount] = (SELECT count(*) FROM [meetings].[MeetingMemberCommentLikes] WHERE [MeetingCommentId] = @MeetingCommentId) 
                               WHERE [Id] = @MeetingCommentId;
                               """;

            await connection.ExecuteAsync(
                sql,
                new
                {
                    MeetingCommentId = notification.DomainEvent.MeetingCommentId.Value
                });
        }
    }
}