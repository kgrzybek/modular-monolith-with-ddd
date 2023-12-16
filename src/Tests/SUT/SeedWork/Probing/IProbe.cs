namespace CompanyName.MyMeetings.SUT.SeedWork.Probing
{
    public interface IProbe
    {
        bool IsSatisfied();

        Task SampleAsync();

        string DescribeFailureTo();
    }

    public interface IProbe<T>
    {
        bool IsSatisfied(T sample);

        Task<T> GetSampleAsync();

        string DescribeFailureTo();
    }
}