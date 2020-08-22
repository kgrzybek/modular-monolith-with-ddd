using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.MeetingComments.GetMeetingComments
{
    public class GetMeetingCommentsQueryHandler : IQueryHandler<GetMeetingCommentsQuery, List<MeetingCommentDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingCommentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingCommentDto>> Handle(GetMeetingCommentsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[MeetingComment].[Id], " +
                               "[MeetingComment].[MeetingId], " +
                               "[MeetingComment].[AuthorId], " +
                               "[MeetingComment].[InReplyToCommentId], " +
                               "[MeetingComment].[Comment], " +
                               "[MeetingComment].[CreateDate], " +
                               "[MeetingComment].[EditDate]" +
                               "FROM [meetings].[v_MeetingComments] AS [MeetingComment]";
            var meetingComments = await connection.QueryAsync<MeetingCommentDto>(sql);

            return meetingComments.AsList();
        }
    }
}