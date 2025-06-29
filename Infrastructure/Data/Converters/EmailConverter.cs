using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Converters
{
    public class EmailConverter : ValueConverter<Email, string>
    {
        public EmailConverter()
        : base(
            email => email.Value,
            value => Email.Create(value))
        {
        }
    }
}
