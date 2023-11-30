using System;
using System.Dynamic;
using MediatR;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.Contracts
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        Guid Id { get; }
    }

    public interface ICommand : IRequest
    {
        Guid Id { get; }
    }
}