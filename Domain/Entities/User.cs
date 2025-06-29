using Domain.ValueObjects;
using SharedKernel.Constants;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public PasswordHash Password { get; set; }
        public Email Email { get; set; }
        public Roles Role { get; set; }
        public virtual List<Task> Tasks { get; set; } = [];

    }
}
