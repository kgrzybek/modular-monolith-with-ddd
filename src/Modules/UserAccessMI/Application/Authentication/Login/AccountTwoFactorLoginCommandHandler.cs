using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.ErrorHandling;
using CompanyName.MyMeetings.Modules.UserAccessMI.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.Login;

internal class AccountTwoFactorLoginCommandHandler : ICommandHandler<AccountTwoFactorLoginCommand, AuthenticationResult>
{
    private readonly ITokenClaimsService _tokenClaimsService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    public AccountTwoFactorLoginCommandHandler(
        UserManager<ApplicationUser> userManager,
        ITokenClaimsService tokenClaimsService,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
    {
        _userManager = userManager;
        _tokenClaimsService = tokenClaimsService;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task<AuthenticationResult> Handle(AccountTwoFactorLoginCommand request, CancellationToken cancellationToken)
    {
        var response = new AuthenticationResult();

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
        {
            response.AddError(Errors.General.NotFound(request.UserId, "User"));
            return response;
        }

        var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, request.Provider, request.Token);

        if (isValid)
        {
            var userDto = new UserDto()
            {
                Id = user.Id,
                Name = $"{user.FirstName} {user.LastName}",
                UserName = user.UserName,
                Email = user.Email,
                Claims = _tokenClaimsService.GetUserClaims(user)
            };

            var tokens = _tokenClaimsService.GenerateTokens(user);
            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            response.SetAuthenticatedUser(userDto, tokens.AccessToken, tokens.RefreshToken, principal);
        }

        return response;
    }
}