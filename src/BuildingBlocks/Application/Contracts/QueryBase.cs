using System;

namespace CompanyName.MyMeetings.BuildingBlocks.Application.Contracts
{
    public abstract class QueryBase<TResult> : IQuery<TResult>
    {
        public Guid Id { get; }

        protected QueryBase()
        {
            this.Id = Guid.NewGuid();
        }

        protected QueryBase(Guid id)
        {
            this.Id = id;
        }
    }
}