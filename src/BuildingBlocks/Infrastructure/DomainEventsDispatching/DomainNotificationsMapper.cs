namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    /// <summary>
    /// Maps domain notification types to their corresponding names and vice versa.
    /// </summary>
    public class DomainNotificationsMapper : IDomainNotificationsMapper
    {
        private readonly BiDictionary<string, Type> _domainNotificationsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainNotificationsMapper"/> class.
        /// </summary>
        /// <param name="domainNotificationsMap">The domain notifications map.</param>
        public DomainNotificationsMapper(BiDictionary<string, Type> domainNotificationsMap)
        {
            _domainNotificationsMap = domainNotificationsMap;
        }

        /// <summary>
        /// Gets the name associated with the specified domain notification type.
        /// </summary>
        /// <param name="type">The domain notification type.</param>
        /// <returns>The name associated with the domain notification type, or null if not found.</returns>
        public string GetName(Type type)
        {
            return _domainNotificationsMap.TryGetBySecond(type, out var name) ? name : null;
        }

        /// <summary>
        /// Gets the domain notification type associated with the specified name, if any.
        /// </summary>
        /// <param name="name">The name of the domain notification.</param>
        /// <returns>The domain notification type associated with the name, or null if not found.</returns>
        public Type GetType(string name)
        {
            return _domainNotificationsMap.TryGetByFirst(name, out var type) ? type : null;
        }
    }
}