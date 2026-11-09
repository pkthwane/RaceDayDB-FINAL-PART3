using Microsoft.AspNetCore.Mvc;
using RaceDayMVC.Models;
using RaceDayMVC.Services;

namespace RaceDayMVC.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApiService _apiService;
        private readonly BlobStorageService _blobStorageService;

        private readonly IWebHostEnvironment _environment;

        public RaceController(
            ApiService apiService,
            BlobStorageService blobStorageService,
            IWebHostEnvironment environment)
        {
            _apiService = apiService;
            _blobStorageService = blobStorageService;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var races = await _apiService.GetAsync<List<Race>>("Race");

            return View(races);
        }

        public async Task<IActionResult> Details(int id)
        {
            var race = await _apiService.GetAsync<Race>($"Race/{id}");

            if (race == null)
                return NotFound();

            return View(race);
        }

        [HttpPost]
        public async Task<IActionResult> Register(int id)
        {
            var registration = new Registration
            {
                RunnerID = 1,
                RaceID = id,
                CategoryID = 1,
                PaymentStatus = "Pending"
            };

            var success = await _apiService.PostAsync(
                "Registration",
                registration);

            if (success)
            {
                TempData["Success"] =
                    "Successfully registered for race!";
            }
            else
            {
                TempData["Success"] =
                    "Registration failed!";
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
                return View(race);

            // Upload banner image
            if (race.BannerFile != null)
            {
                string fileName =
                    Guid.NewGuid().ToString() +
                    Path.GetExtension(race.BannerFile.FileName);

                if (race.BannerFile != null)
                {
                    race.BannerImageUrl =
                        await _blobStorageService
                            .UploadFileAsync(race.BannerFile);
                }

            }

            var success = await _apiService.PostAsync(
                "Race",
                race);

            if (success)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(race);
        }
    }
}