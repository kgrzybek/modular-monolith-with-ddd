using System;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Application.Configuration.Commands
{
    public abstract class InternalCommandBase : ICommand
    {
        protected InternalCommandBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }

    public abstract class InternalCommandBase<TResult> : ICommand<TResult>
    {
        protected InternalCommandBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected InternalCommandBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}