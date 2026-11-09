using Microsoft.AspNetCore.Mvc;
using RaceDayDB.Data;

namespace RaceDayDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public DashboardController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetDashboard()
        {
            var dashboard = new
            {
                TotalRaces = _context.Races.Count(),

                TotalParticipants = _context.Registrations.Count(),

                CompletedRaces = _context.Races.Count(r =>
                    r.Date < DateTime.Today),

                UpcomingRaces = _context.Races.Count(r =>
                    r.Date >= DateTime.Today)
            };

            return Ok(dashboard);
        }
    }
}