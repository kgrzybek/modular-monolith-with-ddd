using System;
using System.Dynamic;
using MediatR;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Contracts
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        Guid Id { get; }
    }

    public interface ICommand : IRequest<Unit>
    {
        Guid Id { get; }
    }
}