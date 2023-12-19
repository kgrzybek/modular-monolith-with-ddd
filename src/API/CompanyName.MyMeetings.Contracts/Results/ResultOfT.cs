namespace CompanyName.MyMeetings.Contracts.Results;

#pragma warning disable SA1649 // File name should match first type name
public class Result<T> : Result
#pragma warning restore SA1649 // File name should match first type name
    where T : notnull
{
    public Result()
        : base()
    {
    }

    internal Result(T? value, IDictionary<string, IEnumerable<ErrorMessage>>? errors)
        : base(value, errors)
    {
    }

    public new T? Value { get; set; }
}