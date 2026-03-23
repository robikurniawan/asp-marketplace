using AspMarketplace.Web.Controllers;
using AspMarketplace.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspMarketplace.Web.Areas.Public.Controllers;

[Area("Public")]
public class HomeController(ICurrentUserService currentUser) : BaseController(currentUser)
{
    public IActionResult Index()
    {
        return View();
    }
}
