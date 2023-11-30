namespace CompanyName.MyMeetings.Modules.Payments.Application.Payers.GetPayer
{
    public class PayerDto
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }
    }
}