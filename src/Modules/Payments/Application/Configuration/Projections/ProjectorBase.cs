using System.Threading.Tasks;
using CompanyName.MyMeetings.Modules.Payments.Domain.SeedWork;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Projections
{
    internal abstract class ProjectorBase
    {
        protected static Task When(IDomainEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}