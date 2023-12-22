using System;
using System.Collections.Generic;

namespace CompanyName.MyMeetings.BuildingBlocks.Application
{
    public class InvalidCommandException : Exception
    {
        public List<string> Errors { get; }

        public InvalidCommandException(List<string> errors)
        {
            this.Errors = errors;
        }
    }
}
