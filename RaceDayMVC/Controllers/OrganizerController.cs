using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class OrganizerController : Controller
    {
        private readonly ApiService _apiService;

        public OrganizerController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var dashboard =
                await _apiService.GetAsync<Dashboard>(
                    "Organizer/1/dashboard");

            return View(dashboard);
        }
    }
}
