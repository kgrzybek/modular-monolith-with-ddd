using System;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Processing.InternalCommands
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