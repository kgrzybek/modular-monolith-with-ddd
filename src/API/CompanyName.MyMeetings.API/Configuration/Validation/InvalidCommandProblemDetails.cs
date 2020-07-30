using System.Collections.Generic;
using CompanyName.MyMeetings.BuildingBlocks.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Configuration.Validation
{
    public class InvalidCommandProblemDetails : ProblemDetails
    {
        public List<string> Errors { get; }

        public InvalidCommandProblemDetails(InvalidCommandException exception)
        {
            Title = "Command validation error";
            Status = StatusCodes.Status400BadRequest;
            Type = "https://somedomain/validation-error";
            Errors = exception.Errors;
        }
    }
}