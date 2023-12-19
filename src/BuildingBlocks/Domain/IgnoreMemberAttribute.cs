namespace CompanyName.MyMeetings.BuildingBlocks.Domain
{
    /// <summary>
    /// Specifies that a property or field should be ignored during member mapping or serialization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}