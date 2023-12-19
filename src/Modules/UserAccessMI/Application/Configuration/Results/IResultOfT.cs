namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;

#pragma warning disable SA1649 // File name should match first type name
public interface IResult<T> : IResult
#pragma warning restore SA1649 // File name should match first type name
{
    T? Value { get; }
}
