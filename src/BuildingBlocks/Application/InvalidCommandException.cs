using System;

namespace CompanyName.MyMeetings.BuildingBlocks.Application
{
    public class InvalidCommandException : Exception
    {
        public string Details { get; }

        public InvalidCommandException(string message, string details) : base(message)
        {
            this.Details = details;
        }
    }
}
