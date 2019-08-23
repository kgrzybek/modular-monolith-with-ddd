using System;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts
{
    public abstract class QueryBase
    {
        public Guid Id { get; }

        protected QueryBase()
        {
            this.Id = Guid.NewGuid();
        }
    }
}