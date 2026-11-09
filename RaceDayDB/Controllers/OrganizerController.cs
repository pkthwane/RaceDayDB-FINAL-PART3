using Microsoft.AspNetCore.Mvc;
using RaceDayDB.Data;
using RaceDayDB.Models;
using Microsoft.EntityFrameworkCore;

namespace RaceDayDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public OrganizerController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetOrganizers()
        {
            var organizers = _context.Organizers
                .Select(o => new
                {
                    o.OrgID,
                    o.Name,
                    o.ContactInfo,
                    o.Email
                })
                .ToList();

            return Ok(organizers);
        }
        [HttpGet("{id}/dashboard")]
        public async Task<IActionResult> GetDashboard(int id)
        {
            var races = await _context.Races
                .Where(r => r.OrgID == id)
                .ToListAsync();

            var raceIds = races.Select(r => r.RaceID).ToList();

            var participantCount = await _context.Registrations
                .CountAsync(r => raceIds.Contains(r.RaceID));

            var completedRaces = races.Count(r => r.Date < DateTime.Now);
            var upcomingRaces = races.Count(r => r.Date >= DateTime.Now);

            return Ok(new
            {
                TotalRaces = races.Count,
                TotalParticipants = participantCount,
                CompletedRaces = completedRaces,
                UpcomingRaces = upcomingRaces
            });
        }
    }
}