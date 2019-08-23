using System;
using CompanyName.MyMeetings.BuildingBlocks.Domain;

namespace CompanyName.MyMeetings.Modules.UserAccess.Domain.UserRegistration
{
    public class UserRegistrationId : TypedIdValueBase
    {
        public UserRegistrationId(Guid value) : base(value)
        {
        }
    }
}