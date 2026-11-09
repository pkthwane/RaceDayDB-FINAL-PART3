using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayDB.Controllers;
using RaceDayDB.Data;
using RaceDayDB.Models;

namespace RaceDayDb.Tests.Controllers
{
    public class RegistrationControllerTests
    {
        private RaceDayDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<RaceDayDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new RaceDayDbContext(options);
        }

        [Fact]
        public void Registration_IsSavedToDatabase()
        {
            var context = GetDbContext();

            context.Registrations.Add(
                new Registration
                {
                    RunnerID = 1,
                    RaceID = 1,
                    CategoryID = 1,
                    PaymentStatus = "Paid"
                });

            context.SaveChanges();

            Assert.Equal(
                1,
                context.Registrations.Count());
        }

        [Fact]
        public void AddRegistration_CreatesRegistration()
        {
            var context = GetDbContext();

            var controller =
                new RegistrationController(context);

            var registration = new Registration
            {
                RunnerID = 1,
                RaceID = 1,
                CategoryID = 1,
                PaymentStatus = "Paid"
            };

            var result =
                controller.AddRegistration(registration);

            Assert.IsType<CreatedAtActionResult>(result);
        }
    }
}