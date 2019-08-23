using System;
using System.Threading.Tasks;

namespace CompanyName.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}