using AspMarketplace.Web.Interfaces;
using System.Security.Claims;

namespace AspMarketplace.Web.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

    public string? UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? UserName => User?.FindFirstValue(ClaimTypes.Name);
    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;
}
