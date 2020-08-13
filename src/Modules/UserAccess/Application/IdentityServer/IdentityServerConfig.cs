using System.Collections.Generic;
using System.Security.Claims;
using CompanyName.MyMeetings.Modules.UserAccess.Application.Contracts;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace CompanyName.MyMeetings.Modules.UserAccess.Application.IdentityServer
{
    public class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("myMeetingsAPI", "My Meetings API")
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
                    AllowedScopes = {
                        "myMeetingsAPI",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        }
    }
}