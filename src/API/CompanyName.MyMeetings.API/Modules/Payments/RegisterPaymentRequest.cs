using System;

namespace CompanyName.MyMeetings.API.Modules.Payments
{
    public class RegisterPaymentRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}