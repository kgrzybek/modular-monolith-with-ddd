using System.Collections.Generic;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public interface IDomainEventsAccessor
    {
        List<IDomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}