namespace Domain.ValueObjects
{
    public class PasswordHash
    {
        public string Hash { get; private set; }

        private PasswordHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException("Password hash cannot be null or empty.");

            Hash = hash;
        }

        public static PasswordHash Create(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return new PasswordHash(hashedPassword);
        }

        public bool Verify(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.");

            return BCrypt.Net.BCrypt.Verify(password, Hash);
        }
    }
}
