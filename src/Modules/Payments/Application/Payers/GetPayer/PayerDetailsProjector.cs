using System.Data;
using CompanyName.MyMeetings.BuildingBlocks.Application.Data;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Projections;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers.Events;
using Dapper;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayer
{
    internal class PayerDetailsProjector : ProjectorBase, IProjector
    {
        private readonly IDbConnection _connection;

        public PayerDetailsProjector(ISqlConnectionFactory sqlConnectionFactory)
        {
            _connection = sqlConnectionFactory.GetOpenConnection();
        }

        public async Task Project(IDomainEvent @event)
        {
            await When((dynamic)@event);
        }

        private async Task When(PayerCreatedDomainEvent payerCreated)
        {
            await _connection.ExecuteScalarAsync(
                "INSERT INTO payments.Payers " +
                "([Id], [Login], [Email], [FirstName], [LastName], " +
                "[Name]) " +
                "VALUES (@PayerId, @Login, @Email, @FirstName, @LastName," +
                "@Name)",
                new
                {
                    payerCreated.PayerId,
                    payerCreated.FirstName,
                    payerCreated.LastName,
                    payerCreated.Email,
                    payerCreated.Login,
                    payerCreated.Name
                });
        }
    }
}