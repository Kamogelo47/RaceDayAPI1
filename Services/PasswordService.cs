
using Microsoft.AspNetCore.Identity;

namespace RaceDayAPI.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<object> _passwordHasher = new();

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(
                new object(),
                password
            );
        }

        public bool VerifyPassword(
            string hashedPassword,
            string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                new object(),
                hashedPassword,
                providedPassword
            );

            return result != PasswordVerificationResult.Failed;
        }
    }
}
