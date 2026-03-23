using AspMarketplace.Web.Controllers;
using AspMarketplace.Web.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspMarketplace.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class DashboardController(ICurrentUserService currentUser) : BaseController(currentUser)
{
    public IActionResult Index()
    {
        return View();
    }
}
