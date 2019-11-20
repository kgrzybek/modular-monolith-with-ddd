using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;
using Dapper;
using MediatR;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public ProcessInternalCommandsCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[Command].[Type], " +
                               "[Command].[Data] " +
                               "FROM [meetings].[InternalCommands] AS [Command] " +
                               "WHERE [Command].[ProcessedDate] IS NULL";
            var commands = await connection.QueryAsync<InternalCommandDto>(sql);

            var internalCommandsList = commands.AsList();

            foreach (var internalCommand in internalCommandsList)
            {
                Type type = Assembly.GetExecutingAssembly().GetType(internalCommand.Type);
                var commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type) as ICommand;
                
                await CommandsExecutor.Execute(commandToProcess);
            }

            return Unit.Value;
        }

        private class InternalCommandDto
        {
            public string Type { get; set; }

            public string Data { get; set; }
        }
    }
}