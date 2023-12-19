using CompanyName.MyMeetings.Modules.UserAccessIS.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.UserAccessIS.Application.Users.GetUser
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