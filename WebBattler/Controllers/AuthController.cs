using WebBattler.Models.Auth;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebBattler.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebBattler.Controllers;

public class AuthController : Controller
{
    private readonly IAdminAccountService _adminAccountService;

    public AuthController(IAdminAccountService adminAccountService)
    {
        _adminAccountService = adminAccountService;
    }

    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }

    [HttpPost("Login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var account = _adminAccountService.GetByDiscordUserId(model.DiscordUserId);
        if (account == null || !_adminAccountService.VerifyPassword(account, model.Password))
        {
            ModelState.AddModelError(string.Empty, "Неверный Discord User ID или пароль.");
            return View(model);
        }

        await SignInAsync(account.DiscordUserId, account.DisplayName);
        return RedirectToAction("Index", "Admin");
    }

    [HttpGet("Register")]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost("Register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var exists = _adminAccountService.ExistsByDiscordUserId(model.DiscordUserId);
        if (exists)
        {
            ModelState.AddModelError(nameof(model.DiscordUserId), "Аккаунт с таким Discord User ID уже существует.");
            return View(model);
        }

        var account = _adminAccountService.Create(model.DiscordUserId, model.DisplayName, model.Password);

        await SignInAsync(account.DiscordUserId, account.DisplayName);
        return RedirectToAction("Index", "Admin");
    }

    [HttpPost("Logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    private async Task SignInAsync(ulong discordUserId, string displayName)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, discordUserId.ToString()),
            new(ClaimTypes.Name, displayName)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}
