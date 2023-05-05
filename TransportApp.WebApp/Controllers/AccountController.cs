using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TransportApp.Application.Repositories;
using TransportApp.Domain;
using TransportApp.WebApp.Models;

namespace TransportApp.WebApp.Controllers;

public class AccountController : Controller
{
    private IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel modelView)
    {
        var user = await _userRepository.LoginAsync(modelView.Username, modelView.Password);

        if(user is not null)
        {
            await AuthenticateAsync(user.Username, user.Role.Name);

            return RedirectToAction("Index", "Route");
        }

        return Forbid();
      
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel modelView)
    {
       await _userRepository.AddUserAsync(new User 
       { 
           Username = modelView.Username,
           Password = modelView.Password
       });

        return RedirectToAction("Login");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("Login");
    }




    public async Task AuthenticateAsync(string username, string role)
    {
        var claims = new List<Claim>
        {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };

        var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }
}
