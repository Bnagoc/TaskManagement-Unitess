namespace Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; private set; }

        private Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be null or empty.");

            if (!IsValid(value))
                throw new ArgumentException("Invalid email format.");

            Value = value;
        }

        public static Email Create(string value)
        {
            return new Email(value);
        }

        private static bool IsValid(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Email other)
            {
                return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
