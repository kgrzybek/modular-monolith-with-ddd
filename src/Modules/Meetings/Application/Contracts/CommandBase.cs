using System;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Contracts
{
    public class CommandBase : ICommand
    {
        public Guid Id { get; }

        public CommandBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected CommandBase(Guid id)
        {
            this.Id = id;
        }
    }
}