namespace CompanyName.MyMeetings.BuildingBlocks.Application.Queries
{
    /// <summary>
    /// Represents the pagination data for a page of results.
    /// </summary>
    public struct PageData
    {
        /// <summary>
        /// Gets the offset of the page.
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Gets the number of items in the next page.
        /// </summary>
        public int Next { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageData"/> struct.
        /// </summary>
        /// <param name="offset">The offset of the page.</param>
        /// <param name="next">The number of items in the next page.</param>
        public PageData(int offset, int next)
        {
            this.Offset = offset;
            this.Next = next;
        }
    }
}