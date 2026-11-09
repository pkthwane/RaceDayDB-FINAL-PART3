using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayDB.Controllers;
using RaceDayDB.Data;
using RaceDayDB.Models;

namespace RaceDayDb.Tests.Controllers
{
    public class RaceControllerTests
    {
        private RaceDayDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<RaceDayDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new RaceDayDbContext(options);
        }

        [Fact]
        public void AddRace_CreatesRace()
        {
            var context = GetDbContext();

            var controller = new RaceController(context);

            var race = new Race
            {
                Name = "POE Test Race",
                Description = "Testing",
                EventType = "Run",
                Date = DateTime.Now,
                Location = "Pretoria",
                Distance = 5,
                OrgID = 1
            };

            var result = controller.AddRace(race);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public void GetRace_ReturnsNotFound_WhenRaceDoesNotExist()
        {
            var context = GetDbContext();

            var controller = new RaceController(context);

            var result = controller.GetRace(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetRaces_ReturnsOkResult()
        {
            var context = GetDbContext();

            context.Races.Add(new Race
            {
                RaceID = 1,
                Name = "Test Race",
                Description = "Test Description",
                EventType = "Run",
                Location = "Pretoria"
            });

            context.SaveChanges();

            var controller = new RaceController(context);

            var result = controller.GetRaces();

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
