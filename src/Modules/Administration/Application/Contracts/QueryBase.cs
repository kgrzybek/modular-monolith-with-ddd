namespace CompanyName.MyMeetings.Modules.Administration.Application.Contracts
{
    /// <summary>
    /// Base class for query objects in the application layer.
    /// </summary>
    /// <typeparam name="TResult">The type of the query result.</typeparam>
    public abstract class QueryBase<TResult> : IQuery<TResult>
    {
        /// <summary>
        /// Gets the unique identifier of the query.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBase{TResult}"/> class with a new unique identifier.
        /// </summary>
        protected QueryBase()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryBase{TResult}"/> class with the specified unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the query.</param>
        protected QueryBase(Guid id)
        {
            Id = id;
        }
    }
}