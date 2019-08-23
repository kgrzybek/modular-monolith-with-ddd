using System;
using CompanyName.MyMeetings.Modules.Meetings.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Meetings.Application.Configuration.Processing.InternalCommands
{
    internal abstract class InternalCommandBase : ICommand
    {
        public Guid Id { get; }

        protected InternalCommandBase(Guid id)
        {
            this.Id = id;
        }
    }
}