using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Commands;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Configuration.Results;
using CompanyName.MyMeetings.Modules.UserAccessMI.Application.Contracts;
using CSharpFunctionalExtensions;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Application.Authentication.RefreshToken;

internal class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, Configuration.Results.Result<TokenDto>>
{
    private readonly ITokenClaimsService _tokenService;

    public RefreshTokenCommandHandler(ITokenClaimsService tokenService)
    {
        _tokenService = tokenService;
    }

    public async Task<Configuration.Results.Result<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _tokenService.GenerateNewTokensAsync(request.AccessToken, request.RefreshToken, cancellationToken)
            .Map(tokens => new TokenDto(tokens.AccessToken, tokens.RefreshToken));
    }
}