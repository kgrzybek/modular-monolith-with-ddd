using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Payers.CreatePayer
{
    internal class CreatePayerCommandHandler : ICommandHandler<CreatePayerCommand, Guid>
    {
        private readonly IAggregateStore _aggregateStore;

        public CreatePayerCommandHandler(IAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public Task<Guid> Handle(CreatePayerCommand request, CancellationToken cancellationToken)
        {
            var payer = Payer.Create(
                request.UserId,
                request.Login,
                request.Email,
                request.FirstName,
                request.LastName,
                request.Name);

            _aggregateStore.AppendChanges(payer);

            return Task.FromResult(payer.Id);
        }
    }
}