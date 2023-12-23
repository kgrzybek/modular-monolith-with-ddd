using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Emails
{
    internal class GetAllEmailsQueryHandler : IQueryHandler<GetAllEmailsQuery, List<EmailDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllEmailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<EmailDto>> Handle(GetAllEmailsQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = $"""
                       SELECT 
                           [Email].[Id] AS [{nameof(EmailDto.Id)}], 
                           [Email].[From] AS [{nameof(EmailDto.From)}], 
                           [Email].[To] AS [{nameof(EmailDto.To)}],
                           [Email].[Subject] AS [{nameof(EmailDto.Subject)}],
                           [Email].[Content] AS [{nameof(EmailDto.Content)}],
                           [Email].[Date] AS [{nameof(EmailDto.Date)}] 
                       FROM [app].[Emails] AS [Email] 
                       ORDER BY [Email].[Date] DESC
                       """;
            var result = await connection.QueryAsync<EmailDto>(sql);
            return result.AsList();
        }
    }
}