using System;
using Microsoft.AspNetCore.Authorization;

namespace CompanyName.MyMeetings.API.Configuration.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    internal class HasPermissionAttribute : AuthorizeAttribute
    {
        internal static string HasPermissionPolicyName = "HasPermission";
        public string Name { get; }

        public HasPermissionAttribute(string name) : base(HasPermissionPolicyName)
        {
            Name = name;
        }
    }
}