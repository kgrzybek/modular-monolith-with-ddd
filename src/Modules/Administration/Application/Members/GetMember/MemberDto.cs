namespace CompanyName.MyMeetings.Modules.Administration.Application.Members.GetMember
{
    public class MemberDto
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }
    }
}