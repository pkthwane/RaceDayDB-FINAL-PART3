using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApiService _apiService;

        public RegistrationController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var registrations =
                await _apiService.GetAsync<List<Registration>>(
                    "Registration");

            return View(registrations);
        }
    }
}