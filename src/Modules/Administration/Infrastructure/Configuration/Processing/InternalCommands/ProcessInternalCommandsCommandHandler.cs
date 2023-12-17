using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using Dapper;
using Newtonsoft.Json;
using Polly;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommandHandler : ICommandHandler<ProcessInternalCommandsCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IInternalCommandsMapper _internalCommandsMapper;

        public ProcessInternalCommandsCommandHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IInternalCommandsMapper internalCommandsMapper)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _internalCommandsMapper = internalCommandsMapper;
        }

        public async Task Handle(ProcessInternalCommandsCommand command, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            string sql = $"""
                          SELECT [Command].[Id] AS [{nameof(InternalCommandDto.Id)}], 
                                 [Command].[Type] AS [{nameof(InternalCommandDto.Type)}], 
                                 [Command].[Data] AS [{nameof(InternalCommandDto.Data)}] 
                          FROM [administration].[InternalCommands] AS [Command] 
                          WHERE [Command].[ProcessedDate] IS NULL 
                          ORDER BY [Command].[EnqueueDate]
                          """;

            var commands = await connection.QueryAsync<InternalCommandDto>(sql);

            var internalCommandsList = commands.AsList();
            var policy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
                });
            foreach (var internalCommand in internalCommandsList)
            {
                var result = await policy.ExecuteAndCaptureAsync(() => ProcessCommand(
                    internalCommand));

                if (result.Outcome == OutcomeType.Failure)
                {
                    const string updateOnErrorSql = "UPDATE [administration].[InternalCommands] " +
                                                    "SET ProcessedDate = @NowDate, " +
                                                    "Error = @Error " +
                                                    "WHERE [Id] = @Id";

                    await connection.ExecuteScalarAsync(
                        updateOnErrorSql,
                        new
                        {
                            NowDate = DateTime.UtcNow,
                            Error = result.FinalException.ToString(),
                            internalCommand.Id
                        });
                }
            }
        }

        private async Task ProcessCommand(
            InternalCommandDto internalCommand)
        {
            var type = _internalCommandsMapper.GetType(internalCommand.Type);
            dynamic commandToProcess = JsonConvert.DeserializeObject(internalCommand.Data, type);

            await CommandsExecutor.Execute(commandToProcess);
        }

        private class InternalCommandDto
        {
            public Guid Id { get; set; }

            public string Type { get; set; }

            public string Data { get; set; }
        }
    }
}