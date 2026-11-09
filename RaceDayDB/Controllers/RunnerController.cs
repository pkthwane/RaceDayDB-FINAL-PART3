using Microsoft.AspNetCore.Mvc;
using RaceDayDB.Data;
using RaceDayDB.Models;

namespace RaceDayDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunnerController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public RunnerController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetRunners()
        {
            var runners = _context.Runners.ToList();
            return Ok(runners);
        }

        [HttpGet("{id}")]
        public IActionResult GetRunner(int id)
        {
            var runner = _context.Runners.Find(id);

            if (runner == null)
            {
                return NotFound();
            }

            return Ok(runner);
        }

        [HttpPost]
        public IActionResult AddRunner(Runner runner)
        {
            _context.Runners.Add(runner);
            _context.SaveChanges();

            return CreatedAtAction(
                nameof(GetRunner),
                new { id = runner.RunnerID },
                runner);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRunner(int id, Runner updatedRunner)
        {
            var runner = _context.Runners.Find(id);

            if (runner == null)
            {
                return NotFound();
            }

            runner.Name = updatedRunner.Name;
            runner.Age = updatedRunner.Age;
            runner.Gender = updatedRunner.Gender;
            runner.ContactInfo = updatedRunner.ContactInfo;
            runner.Email = updatedRunner.Email;
            runner.PasswordHash = updatedRunner.PasswordHash;

            _context.SaveChanges();

            return Ok(runner);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRunner(int id)
        {
            var runner = _context.Runners.Find(id);

            if (runner == null)
            {
                return NotFound();
            }

            _context.Runners.Remove(runner);
            _context.SaveChanges();

            return Ok("Runner deleted successfully.");
        }
    }
}