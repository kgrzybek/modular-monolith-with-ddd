using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Projections
{
    public interface IProjector
    {
        Task Project(IDomainEvent @event);
    }
}