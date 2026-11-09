using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;
using RaceDayMVC.ViewModels;

namespace RaceDayMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;

        public AccountController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginRequest = new LoginRequest
            {
                Email = model.Email,
                Password = model.Password
            };

            var success = await _apiService.PostAsync(
                "Auth/login",
                loginRequest);

            if (success)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid Login");

            return View(model);
        }
    }
}
