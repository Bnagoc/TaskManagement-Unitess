namespace Domain.Interfaces
{
    public interface ITaskRepository
    {
        IQueryable<Domain.Entities.Task> GetTasksQuery();
        Task<Domain.Entities.Task?> GetByIdAsync(Guid id);
        Task AddAsync(Entities.Task task);
        Task UpdateAsync(Entities.Task task);
        Task DeleteAsync(Guid id);
    }
}
