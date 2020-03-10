using CompanyName.MyMeetings.BuildingBlocks.Domain;
using Microsoft.AspNetCore.Http;

namespace CompanyName.MyMeetings.API.Configuration.Validation
{
    public class BusinessRuleValidationExceptionProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            this.Title = "Business rule broken";
            this.Status = StatusCodes.Status409Conflict;
            this.Detail = exception.Message;
            this.Type = "https://somedomain/business-rule-validation-error";
        }
    }
}