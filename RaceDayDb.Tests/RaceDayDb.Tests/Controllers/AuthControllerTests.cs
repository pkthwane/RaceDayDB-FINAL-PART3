namespace RaceDayDb.Tests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public void PasswordHashVerification_Succeeds()
        {
            string password = "Password123";

            string hash =
                BCrypt.Net.BCrypt.HashPassword(password);

            bool result =
                BCrypt.Net.BCrypt.Verify(password, hash);

            Assert.True(result);
        }

        [Fact]
        public void PasswordHashVerification_Fails()
        {
            string password = "Password123";

            string hash =
                BCrypt.Net.BCrypt.HashPassword(password);

            bool result =
                BCrypt.Net.BCrypt.Verify(
                    "WrongPassword",
                    hash);

            Assert.False(result);
        }
    }
}