using CompanyName.MyMeetings.Modules.UserAccessMI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CompanyName.MyMeetings.Modules.UserAccessMI.Infrastructure.Domain.Repositories;

internal class UserRefreshTokenRepository : IUserRefreshTokenRepository
{
    private readonly UserAccessContext _context;

    public UserRefreshTokenRepository(UserAccessContext context)
    {
        _context = context;
    }

    public Task<UserRefreshToken?> GetByJwtIdAsync(string id, CancellationToken cancellationToken)
    {
        return _context.UserRefreshTokens
            .SingleOrDefaultAsync(x => x.JwtId == id, cancellationToken);
    }

    public void Add(UserRefreshToken userRefreshToken)
    {
        _context.UserRefreshTokens.Add(userRefreshToken);
    }

    public Task<int> DeleteAsync(UserRefreshToken userRefreshToken, CancellationToken cancellationToken = default)
    {
        _context.UserRefreshTokens.Remove(userRefreshToken);
        return _context.SaveChangesAsync(cancellationToken);
    }
}