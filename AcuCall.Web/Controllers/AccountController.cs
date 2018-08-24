using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AcuCall.Core.Interfaces;
using AcuCall.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AcuCall.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public AccountController(IUserService userService, IUserSessionService userSessionService)
        {
            _userService = userService;
            _userSessionService = userSessionService;
        }

        public IActionResult Login()
        {           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindUserByCredentialsAsync(loginViewModel.Username, loginViewModel.Password);

                if (user != null)
                {
                    int sessionId = await _userSessionService.AddSession(user.Username);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Sid, sessionId.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View(loginViewModel);
                }
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            int sessionId = Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value);
            await _userSessionService.Logout(sessionId);
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}