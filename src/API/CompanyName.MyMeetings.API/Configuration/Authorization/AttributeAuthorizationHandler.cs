using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace CompanyName.MyMeetings.API.Configuration.Authorization
{
    public abstract class AttributeAuthorizationHandler<TRequirement, TAttribute>
        : AuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement
        where TAttribute : Attribute
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var attribute = (context.Resource as RouteEndpoint)?.Metadata.GetMetadata<TAttribute>();

            return HandleRequirementAsync(context, requirement, attribute);
        }

        protected abstract Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            TRequirement requirement,
            TAttribute attribute);
    }
}