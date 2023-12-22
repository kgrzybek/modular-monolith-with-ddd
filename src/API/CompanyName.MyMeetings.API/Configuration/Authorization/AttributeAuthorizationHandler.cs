using Microsoft.AspNetCore.Authorization;

namespace CompanyName.MyMeetings.API.Configuration.Authorization
{
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute>
        : AuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement
        where TAttribute : Attribute
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var endpoint = (context.Resource as HttpContext).GetEndpoint() as RouteEndpoint;
            var attribute = endpoint?.Metadata.GetMetadata<TAttribute>();

            return HandleRequirementAsync(context, requirement, attribute);
        }

        protected abstract Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            TRequirement requirement,
            TAttribute attribute);
    }
}