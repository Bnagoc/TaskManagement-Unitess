using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async System.Threading.Tasks.Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            var users = await _context.Users.ToListAsync();
            return users.Any(u => u.Email.Value.Contains(email));
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var users = await _context.Users.ToListAsync();
            return users.FirstOrDefault(u => u.Email.Value.Contains(email));

        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId);

        }

        public IQueryable<User> GetUsersQuery()
        {
            return _context.Users
                .Include(u => u.Tasks)
                .AsQueryable();
        }

    }
}
