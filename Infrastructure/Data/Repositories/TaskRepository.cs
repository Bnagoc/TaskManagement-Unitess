using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Domain.Entities.Task> GetTasksQuery()
        {
            var query = _context.Tasks
                .Include(t => t.Users)
                .Include(t => t.CreatedBy)
                .AsQueryable();

            return query;
        }

        public async Task<Domain.Entities.Task?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks
                .Include(t => t.Users)
                .Include(t => t.CreatedBy)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Domain.Entities.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
