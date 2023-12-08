using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Users.GetUser;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.GetAuthenticatedUser
{
    public class GetAuthenticatedUserQuery : QueryBase<UserDto>
    {
        public GetAuthenticatedUserQuery()
        {
        }
    }
}