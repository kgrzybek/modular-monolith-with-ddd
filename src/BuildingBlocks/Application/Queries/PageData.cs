namespace CompanyName.MyMeetings.BuildingBlocks.Application.Queries
{
    public struct PageData
    {
        public int Offset { get; }

        public int Next { get; }

        public PageData(int offset, int next)
        {
            this.Offset = offset;
            this.Next = next;
        }
    }
}