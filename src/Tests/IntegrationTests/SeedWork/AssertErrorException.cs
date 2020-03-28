using System;

namespace CompanyName.MyMeetings.IntegrationTests.SeedWork
{
    public class AssertErrorException : Exception
    {
        public AssertErrorException(string message) : base(message)
        {
            
        }
    }
}