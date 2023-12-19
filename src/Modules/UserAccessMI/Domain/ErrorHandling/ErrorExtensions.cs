using CSharpFunctionalExtensions;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;

public static class ErrorExtensions
{
    public static UnitResult<Error> ToErrorOrUnit(this IEnumerable<Error> errors)
    {
        if (errors.Any())
        {
            return errors.Combine();
        }

        return UnitResult.Success<Error>();
    }

    public static Error Combine(this IEnumerable<Error> errors)
        => MergeErrors(errors);

    private static Error MergeErrors(IEnumerable<Error> errors)
    {
        if (errors is null)
        {
            throw new ArgumentNullException(nameof(errors));
        }

        if (errors.Count() <= 0)
        {
            return new Error();
        }

        // Take the first error of the collection
        var errorDestination = errors.First();

        if (errors.Count() == 1)
        {
            return errorDestination;
        }

        // and merge the remaining one's into the first one
        for (int i = 1; i < errors.Count(); i++)
        {
            var errorSource = errors.ElementAt(i);
            errorSource = MergeChildren(errorSource);

            errorDestination.Combine(errorSource);
        }

        return errorDestination;
    }

    private static Error MergeChildren(Error parent)
    {
        if (parent is null)
        {
            throw new ArgumentNullException(nameof(parent));
        }

        var children = parent.Errors;
        if (children is not null)
        {
            foreach (var child in children)
            {
                MergeChildren(child);
                parent.Combine(child);
            }
        }

        return parent;
    }
}