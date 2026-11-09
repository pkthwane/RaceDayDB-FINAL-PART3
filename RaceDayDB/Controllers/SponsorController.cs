using Microsoft.AspNetCore.Mvc;
using RaceDayDB.Data;
using RaceDayDB.Models;
using Microsoft.AspNetCore.Authorization;

namespace RaceDayDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public SponsorController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetSponsors()
        {
            var sponsors = _context.Sponsors.ToList();
            return Ok(sponsors);
        }

        [HttpGet("{id}")]
        public IActionResult GetSponsor(int id)
        {
            var sponsor = _context.Sponsors.Find(id);

            if (sponsor == null)
            {
                return NotFound();
            }

            return Ok(sponsor);
        }

        [HttpPost]
        public IActionResult AddSponsor(Sponsor sponsor)
        {
            _context.Sponsors.Add(sponsor);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetSponsor),
                new { id = sponsor.SponsorID },
                sponsor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSponsor(int id, Sponsor updatedSponsor)
        {
            var sponsor = _context.Sponsors.Find(id);

            if (sponsor == null)
            {
                return NotFound();
            }

            sponsor.Name = updatedSponsor.Name;
            sponsor.Email = updatedSponsor.Email;

            _context.SaveChanges();

            return Ok(sponsor);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSponsor(int id)
        {
            var sponsor = _context.Sponsors.Find(id);

            if (sponsor == null)
            {
                return NotFound();
            }

            _context.Sponsors.Remove(sponsor);
            _context.SaveChanges();

            return Ok("Sponsor deleted successfully.");
        }
    }
}
