using AspMarketplace.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AspMarketplace.Web.Controllers;

public abstract class BaseController(ICurrentUserService currentUser) : Controller
{
    protected ICurrentUserService CurrentUser => currentUser;

    protected void SetSuccess(string message) => TempData["SuccessMessage"] = message;
    protected void SetError(string message) => TempData["ErrorMessage"] = message;
    protected void SetWarning(string message) => TempData["WarningMessage"] = message;

    protected string? SuccessMessage => TempData["SuccessMessage"] as string;
    protected string? ErrorMessage => TempData["ErrorMessage"] as string;
}
