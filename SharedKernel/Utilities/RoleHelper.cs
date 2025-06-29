using SharedKernel.Constants;

namespace SharedKernel.Utilities
{
    public class RoleHelper
    {
        public Roles GetRoleFromString(string roleString)
        {
            try
            {
                return (Roles)Enum.Parse(typeof(Roles), roleString, ignoreCase: true);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException($"Invalid role: {roleString}");
            }
        }
    }
}
