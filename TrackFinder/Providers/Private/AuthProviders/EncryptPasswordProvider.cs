using BC = BCrypt.Net.BCrypt;

namespace TrackFinder.Providers.Private.AuthProviders
{
    public class EncryptPasswordProvider : IEncryptPasswordProvider
    {
        private const int WorkFactor = 12;

        public string HashPassword(string plainPassword)
            => BC.HashPassword(plainPassword, WorkFactor);

        public bool VerifyPassword(string plainPassword, string hashedPassword)
            => BC.Verify(plainPassword, hashedPassword);
    }
}