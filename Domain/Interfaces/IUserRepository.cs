using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetUsersQuery();
        Task<bool> ExistsByUsernameAsync(string username);
        Task<User?> GetByIdAsync(Guid userId);
        Task<bool> ExistsByEmailAsync(string email);
        System.Threading.Tasks.Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}
