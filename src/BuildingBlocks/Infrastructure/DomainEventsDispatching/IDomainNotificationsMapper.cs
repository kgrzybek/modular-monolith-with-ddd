namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.DomainEventsDispatching
{
    /// <summary>
    /// Represents a mapper for domain notifications.
    /// </summary>
    public interface IDomainNotificationsMapper
    {
        /// <summary>
        /// Gets the name of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The name of the type.</returns>
        string GetName(Type type);

        /// <summary>
        /// Gets the type with the specified name.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <returns>The type with the specified name.</returns>
        Type GetType(string name);
    }
}