using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands;
using Dapper;
using MediatR;
using Newtonsoft.Json;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration.Processing.Inbox
{
    /// <summary>
    /// Handles the processing of inbox messages.
    /// </summary>
    internal class ProcessInboxCommandHandler : ICommandHandler<ProcessInboxCommand>
    {
        private readonly IMediator _mediator;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessInboxCommandHandler"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="sqlConnectionFactory">The SQL connection factory.</param>
        public ProcessInboxCommandHandler(IMediator mediator, ISqlConnectionFactory sqlConnectionFactory)
        {
            _mediator = mediator;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        /// <summary>
        /// Handles the <see cref="ProcessInboxCommand"/> by retrieving the inbox messages from the database,
        /// deserializing them, and publishing them to the mediator.
        /// </summary>
        /// <param name="command">The <see cref="ProcessInboxCommand"/> object.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Handle(ProcessInboxCommand command, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();
            string sql = "SELECT " +
                         $"[InboxMessage].[Id] AS [{nameof(InboxMessageDto.Id)}], " +
                         $"[InboxMessage].[Type] AS [{nameof(InboxMessageDto.Type)}], " +
                         $"[InboxMessage].[Data] AS [{nameof(InboxMessageDto.Data)}] " +
                         "FROM [administration].[InboxMessages] AS [InboxMessage] " +
                         "WHERE [InboxMessage].[ProcessedDate] IS NULL " +
                         "ORDER BY [InboxMessage].[OccurredOn]";

            var messages = await connection.QueryAsync<InboxMessageDto>(sql);

            const string sqlUpdateProcessedDate = "UPDATE [administration].[InboxMessages] " +
                                                  "SET [ProcessedDate] = @Date " +
                                                  "WHERE [Id] = @Id";

            foreach (var message in messages)
            {
                var messageAssembly = AppDomain.CurrentDomain.GetAssemblies()
                    .SingleOrDefault(assembly => message.Type.Contains(assembly.GetName().Name));

                Type type = messageAssembly.GetType(message.Type);
                var request = JsonConvert.DeserializeObject(message.Data, type);

                try
                {
                    await _mediator.Publish((INotification)request, cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                await connection.ExecuteScalarAsync(sqlUpdateProcessedDate, new
                {
                    Date = DateTime.UtcNow,
                    message.Id
                });
            }
        }
    }
}