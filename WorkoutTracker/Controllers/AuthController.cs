using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WorkoutTracker.Models;
using WorkoutTracker.Services;

namespace WorkoutTracker.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // GET /Auth/Register
        public IActionResult Register() => View();

        // POST /Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _authService.Register(
                model.Username, model.FirstName,
                model.LastName, model.Email, model.Password
            );

            if (user == null)
            {
                ModelState.AddModelError("", "Login lub email już zajęty!");
                return View(model);
            }

            // Automatycznie zaloguj po rejestracji
            await SignInUser(user);
            return RedirectToAction("Index", "Home");
        }

        // GET /Auth/Login
        public IActionResult Login() => View();

        // POST /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _authService.Login(model.Username, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Nieprawidłowy login lub hasło!");
                return View(model);
            }

            await SignInUser(user);
            return RedirectToAction("Index", "Home");
        }

        // POST /Auth/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }

        // Pomocnicza metoda — tworzy cookie z danymi użytkownika
        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);
        }
    }
}