using CompanyName.MyMeetings.BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    /// <summary>
    /// Provides access to domain events in the application.
    /// </summary>
    public class DomainEventsAccessor : IDomainEventsAccessor
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventsAccessor"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public DomainEventsAccessor(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all the domain events that have been recorded.
        /// </summary>
        /// <returns>A read-only collection of domain events.</returns>
        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            var domainEntities = this._dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
        }

        /// <summary>
        /// Clears all the recorded domain events.
        /// </summary>
        public void ClearAllDomainEvents()
        {
            var domainEntities = this._dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }
}