namespace CompanyName.MyMeetings.BuildingBlocks.Domain
{
    public interface IEntity
    {
        void ClearDomainEvents();

        /// <summary>
        /// Domain events occurred.
        /// </summary>
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    }
}
