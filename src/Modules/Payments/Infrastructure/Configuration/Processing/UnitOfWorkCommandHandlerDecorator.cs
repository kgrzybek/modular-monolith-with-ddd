using System;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.Payments.Infrastructure.Configuration.Processing
{
    internal class UnitOfWorkCommandHandlerDecorator<T> : ICommandHandler<T> where T:ICommand
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaymentsContext _paymentsContext;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<T> decorated, 
            IUnitOfWork unitOfWork, 
            PaymentsContext paymentsContext)
        {
            _decorated = decorated;
            _unitOfWork = unitOfWork;
            _paymentsContext = paymentsContext;
        }

        public async Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            await this._decorated.Handle(command, cancellationToken);

            if (command is InternalCommandBase)
            {
                var internalCommand =
                    await _paymentsContext.InternalCommands.FirstOrDefaultAsync(x => x.Id == command.Id,
                        cancellationToken: cancellationToken);

                if (internalCommand != null)
                {
                    internalCommand.ProcessedDate = DateTime.UtcNow;
                }
            }

            await this._unitOfWork.CommitAsync(cancellationToken);

            return Unit.Value;
        }
    }
}