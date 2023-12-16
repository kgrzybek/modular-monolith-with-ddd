using CompanyName.MyMeetings.BuildingBlocks.Application;

namespace CompanyName.MyMeetings.SUT.SeedWork
{
    public class ExecutionContextMock : IExecutionContextAccessor
    {
        public ExecutionContextMock(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; private set; }

        public Guid CorrelationId { get; }

        public bool IsAvailable { get; }

        public void SetUserId(Guid userId)
        {
            this.UserId = userId;
        }
    }
}