namespace CompanyName.MyMeetings.BuildingBlocks.Domain
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}