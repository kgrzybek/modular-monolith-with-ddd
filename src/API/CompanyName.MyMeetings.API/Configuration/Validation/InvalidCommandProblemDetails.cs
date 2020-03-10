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
            this.Title = "Command validation error";
            this.Status = StatusCodes.Status400BadRequest;
            this.Type = "https://somedomain/validation-error";
            this.Errors = exception.Errors;
        }
    }
}