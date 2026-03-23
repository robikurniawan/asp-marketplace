using AspMarketplace.Web.Controllers;
using AspMarketplace.Web.Interfaces;
using AspMarketplace.Web.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspMarketplace.Web.Areas.Auth.Controllers;

[Area("Auth")]
public class AccountController(ICurrentUserService currentUser) : BaseController(currentUser)
{
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (CurrentUser.IsAuthenticated)
            return RedirectToAction("Index", "Home", new { area = "Public" });

        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // TODO: ganti dengan logika validasi user dari database
        var isValid = model.Email == "admin@marketplace.com" && model.Password == "admin123";

        if (!isValid)
        {
            ModelState.AddModelError(string.Empty, "Email atau password salah.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Name, model.Email),
            new(ClaimTypes.Email, model.Email),
            new(ClaimTypes.Role, "Admin"),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = model.RememberMe,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

        SetSuccess("Berhasil masuk.");

        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            return Redirect(model.ReturnUrl);

        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        SetSuccess("Berhasil keluar.");
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult AccessDenied() => View();
}
