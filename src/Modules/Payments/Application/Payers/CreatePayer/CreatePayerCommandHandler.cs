using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Domain.Payers;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Payers.CreatePayer
{
    internal class CreatePayerCommandHandler : ICommandHandler<CreatePayerCommand>
    {
        private readonly IPayerRepository _payerRepository;

        public CreatePayerCommandHandler(IPayerRepository payerRepository)
        {
            _payerRepository = payerRepository;
        }

        public async Task<Unit> Handle(CreatePayerCommand request, CancellationToken cancellationToken)
        {
            var payer = Payer.Create(request.UserId, request.Login, request.Email, request.FirstName, request.LastName,
                request.Name);

            await _payerRepository.AddAsync(payer);

            return Unit.Value;
        }
    }
}