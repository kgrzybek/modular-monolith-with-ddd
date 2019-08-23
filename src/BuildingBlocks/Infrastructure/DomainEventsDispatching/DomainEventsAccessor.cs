using System.Collections.Generic;
using System.Linq;
using CompanyName.MyMeetings.BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    public class DomainEventsAccessor : IDomainEventsAccessor
    {
        private readonly DbContext _meetingsContext;

        public DomainEventsAccessor(DbContext meetingsContext)
        {
            _meetingsContext = meetingsContext;
        }

        public List<IDomainEvent> GetAllDomainEvents()
        {
            var domainEntities = this._meetingsContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
        }

        public void ClearAllDomainEvents()
        {
            var domainEntities = this._meetingsContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }
}