using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingDetails
{
    internal class GetMeetingDetailsQueryHandler : IQueryHandler<GetMeetingDetailsQuery, MeetingDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<MeetingDetailsDto> Handle(GetMeetingDetailsQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            return await connection.QuerySingleAsync<MeetingDetailsDto>(
                $"""
                 SELECT 
                     [MeetingDetails].[Id] AS [{nameof(MeetingDetailsDto.Id)}], 
                     [MeetingDetails].[MeetingGroupId] AS [{nameof(MeetingDetailsDto.MeetingGroupId)}],
                     [MeetingDetails].[Title] AS [{nameof(MeetingDetailsDto.Title)}], 
                     [MeetingDetails].[TermStartDate] AS [{nameof(MeetingDetailsDto.TermStartDate)}], 
                     [MeetingDetails].[TermEndDate] AS [{nameof(MeetingDetailsDto.TermEndDate)}], 
                     [MeetingDetails].[Description] AS [{nameof(MeetingDetailsDto.Description)}], 
                     [MeetingDetails].[LocationName] AS [{nameof(MeetingDetailsDto.LocationName)}], 
                     [MeetingDetails].[LocationAddress] AS [{nameof(MeetingDetailsDto.LocationAddress)}], 
                     [MeetingDetails].[LocationPostalCode] AS [{nameof(MeetingDetailsDto.LocationPostalCode)}], 
                     [MeetingDetails].[LocationCity] AS [{nameof(MeetingDetailsDto.LocationCity)}], 
                     [MeetingDetails].[AttendeesLimit] AS [{nameof(MeetingDetailsDto.AttendeesLimit)}], 
                     [MeetingDetails].[GuestsLimit] AS [{nameof(MeetingDetailsDto.GuestsLimit)}], 
                     [MeetingDetails].[RSVPTermStartDate] AS [{nameof(MeetingDetailsDto.RSVPTermStartDate)}], 
                     [MeetingDetails].[RSVPTermEndDate] AS [{nameof(MeetingDetailsDto.RSVPTermEndDate)}], 
                     [MeetingDetails].[EventFeeValue] AS [{nameof(MeetingDetailsDto.EventFeeValue)}], 
                     [MeetingDetails].[EventFeeCurrency] AS [{nameof(MeetingDetailsDto.EventFeeCurrency)}] 
                 FROM [meetings].[v_MeetingDetails] AS [MeetingDetails] 
                 WHERE [MeetingDetails].[Id] = @MeetingId
                 """,
                new
                {
                    query.MeetingId
                });
        }
    }
}