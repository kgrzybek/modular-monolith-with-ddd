using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.Users.GetUser
{
    public class GetUserQuery : QueryBase<UserDto>
    {
        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}