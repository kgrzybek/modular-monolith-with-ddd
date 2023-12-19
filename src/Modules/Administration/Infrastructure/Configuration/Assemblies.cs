using System.Reflection;
using CompanyName.MyMeetings.Modules.Administration.Application.Contracts;

namespace CompanyName.MyMeetings.Modules.Administration.Infrastructure.Configuration
{
    /// <summary>
    /// Assemblies used in Administration module.
    /// </summary>
    internal static class Assemblies
    {
        /// <summary>
        /// Application assembly.
        /// </summary>
        public static readonly Assembly Application = typeof(IAdministrationModule).Assembly;
    }
}