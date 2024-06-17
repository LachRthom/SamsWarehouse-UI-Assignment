using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SamsWarehouse.Models.Repositories;
using SamsWarehouse.Models.ViewModels;
using BCrypt.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using SamsWarehouse.Models;
using System.Collections.Generic;

namespace SamsWarehouse.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userRepository.GetUserByEmail(model.Email);
            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                // Store UserId in session cookie
                HttpContext.Session.SetInt32("UserId", user.UserId);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Invalid login attempt.";
            return View(model);  
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_userRepository.IsEmailExists(user.Email))
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return View(user);
                }

                // Hash the password before saving
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
                _userRepository.AddUser(user);
                return RedirectToAction(nameof(Login));
            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
