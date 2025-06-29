using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Converters
{
    public class PasswordConverter : ValueConverter<PasswordHash, string>
    {
        public PasswordConverter()
        : base(
            password => password.Hash,
            hash => PasswordHash.Create(hash))
        {
        }
    }
}
