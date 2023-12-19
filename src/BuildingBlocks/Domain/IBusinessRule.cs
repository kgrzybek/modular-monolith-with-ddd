namespace CompanyName.MyMeetings.BuildingBlocks.Domain
{
    /// <summary>
    /// Represents a business rule that can be evaluated to determine if it is broken.
    /// </summary>
    public interface IBusinessRule
    {
        /// <summary>
        /// Checks if the business rule is broken.
        /// </summary>
        /// <returns>True if the business rule is broken, false otherwise.</returns>
        bool IsBroken();

        /// <summary>
        /// Gets the error message associated with the broken business rule.
        /// </summary>
        string Message { get; }
    }
}