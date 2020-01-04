using System;
using CompanyName.MyMeetings.Modules.Payments.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Payments.Application.Configuration.Commands
{
    public abstract class InternalCommandBase : ICommand
    {
        public Guid Id { get; }

        protected InternalCommandBase(Guid id)
        {
            this.Id = id;
        }
    }
}