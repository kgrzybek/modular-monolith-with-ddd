﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Queries;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Countries
{
    internal class GetAllCountriesQueryHandler : IQueryHandler<GetAllCountriesQuery, List<CountryDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllCountriesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<CountryDto>> Handle(GetAllCountriesQuery query, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            return (await connection.QueryAsync<CountryDto>(
                "SELECT " +
                $"[Country].[Code] AS [{nameof(CountryDto.Code)}], " +
                $"[Country].[Name] AS [{nameof(CountryDto.Name)}] " +
                "FROM [meetings].[v_Countries] AS [Country]")).AsList();
        }
    }
}