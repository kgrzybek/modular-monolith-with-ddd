using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Domain.Users
{
    public class UserId : TypedIdValueBase
    {
        public UserId(Guid value)
            : base(value)
        {
        }
    }
}