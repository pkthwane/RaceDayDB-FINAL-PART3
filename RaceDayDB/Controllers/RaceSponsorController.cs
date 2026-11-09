using Microsoft.AspNetCore.Mvc;
using RaceDayDB.Data;
using RaceDayDB.Models;
using Microsoft.AspNetCore.Authorization;

namespace RaceDayDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceSponsorController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public RaceSponsorController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetRaceSponsors()
        {
            return Ok(_context.RaceSponsors.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetRaceSponsor(int id)
        {
            var raceSponsor = _context.RaceSponsors.Find(id);

            if (raceSponsor == null)
            {
                return NotFound();
            }

            return Ok(raceSponsor);
        }

        [HttpPost]
        public IActionResult AddRaceSponsor(RaceSponsor raceSponsor)
        {
            _context.RaceSponsors.Add(raceSponsor);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetRaceSponsor),
                new { id = raceSponsor.RaceSponsorID },
                raceSponsor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRaceSponsor(int id, RaceSponsor updatedRaceSponsor)
        {
            var raceSponsor = _context.RaceSponsors.Find(id);

            if (raceSponsor == null)
            {
                return NotFound();
            }

            raceSponsor.RaceID = updatedRaceSponsor.RaceID;
            raceSponsor.SponsorID = updatedRaceSponsor.SponsorID;
            raceSponsor.SponsorshipAmount = updatedRaceSponsor.SponsorshipAmount;

            _context.SaveChanges();

            return Ok(raceSponsor);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRaceSponsor(int id)
        {
            var raceSponsor = _context.RaceSponsors.Find(id);

            if (raceSponsor == null)
            {
                return NotFound();
            }

            _context.RaceSponsors.Remove(raceSponsor);
            _context.SaveChanges();

            return Ok("Race sponsor deleted successfully.");
        }
    }
}
