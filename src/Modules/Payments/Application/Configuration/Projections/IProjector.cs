using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Projections
{
    public interface IProjector
    {
        Task Project(IDomainEvent @event);
    }
}