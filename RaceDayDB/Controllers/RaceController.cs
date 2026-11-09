using Microsoft.AspNetCore.Mvc;
using RaceDayDB.Data;
using RaceDayDB.Models;
using Microsoft.AspNetCore.Authorization;


namespace RaceDayDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public RaceController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetRaces()
        {
            var races = _context.Races.ToList();
            return Ok(races);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetRace(int id)
        {
            var race = _context.Races.Find(id);

            if (race == null)
            {
                return NotFound();
            }

            return Ok(race);
        }

        [HttpPost]
        //[Authorize(Roles = "Organizer")]
        public IActionResult AddRace(Race race)
        {
            _context.Races.Add(race);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetRace),
                new { id = race.RaceID },
                race);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Organizer")]
        public IActionResult UpdateRace(int id, Race updatedRace)
        {
            var race = _context.Races.Find(id);

            if (race == null)
            {
                return NotFound();
            }

            race.Name = updatedRace.Name;
            race.Description = updatedRace.Description;
            race.EventType = updatedRace.EventType;
            race.Date = updatedRace.Date;
            race.Location = updatedRace.Location;
            race.Distance = updatedRace.Distance;
            race.OrgID = updatedRace.OrgID;

            _context.SaveChanges();

            return Ok(race);
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Organizer")]
        public IActionResult DeleteRace(int id)
        {
            var race = _context.Races.Find(id);

            if (race == null)
            {
                return NotFound();
            }

            _context.Races.Remove(race);
            _context.SaveChanges();

            return Ok("Race deleted successfully.");
        }
    }
}
