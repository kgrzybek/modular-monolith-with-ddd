namespace CompanyName.MyMeetings.BuildingBlocks.Domain
{
    /// <summary>
    /// Represents an exception that is thrown when a business rule validation fails.
    /// </summary>
    public class BusinessRuleValidationException : Exception
    {
        /// <summary>
        /// Gets the broken business rule.
        /// </summary>
        public IBusinessRule BrokenRule { get; }

        /// <summary>
        /// Gets the details of the validation exception.
        /// </summary>
        public string Details { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRuleValidationException"/> class with the specified broken rule.
        /// </summary>
        /// <param name="brokenRule">The broken business rule.</param>
        public BusinessRuleValidationException(IBusinessRule brokenRule)
            : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
            Details = brokenRule.Message;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
        }
    }
}
