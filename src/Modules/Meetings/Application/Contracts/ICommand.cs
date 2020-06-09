﻿using System;

using MediatR;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Contracts
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        Guid Id { get; }
    }

    public interface ICommand: ICommand<Unit>
    {
    }
}