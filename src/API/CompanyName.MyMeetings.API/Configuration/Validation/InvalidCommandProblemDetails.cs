using CompanyName.MyMeetings.BuildingBlocks.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyName.MyMeetings.API.Configuration.Validation
{
    public class InvalidCommandProblemDetails : ProblemDetails
    {
        public InvalidCommandProblemDetails(InvalidCommandException exception)
        {
            this.Title = exception.Message;
            this.Status = StatusCodes.Status400BadRequest;
            this.Detail = exception.Details;
            this.Type = "https://somedomain/validation-error";
        }
    }
}