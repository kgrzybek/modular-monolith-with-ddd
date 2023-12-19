using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using IdentityServer4;
using IdentityServer4.Models;

namespace CompanyName.MyMeetings.Modules.UserAccess.Infrastructure.IdentityServer
{
    internal class IdentityServerConfig
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new("all", "Can Do All")
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new("myMeetingsAPI", "My Meetings API")
                {
                    Scopes = { "all" }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(CustomClaimTypes.Roles, new List<string>
                {
                    CustomClaimTypes.Roles
                })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "all",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}