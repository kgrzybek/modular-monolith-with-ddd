using System;
using MediatR;

namespace CompanyName.MyMeetings.BuildingBlocks.Domain
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}