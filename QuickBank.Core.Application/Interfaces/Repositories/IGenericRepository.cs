namespace QuickBank.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task DeleteAsync(Entity entity);
        Task<List<Entity>> GetAllAsync();
        Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties);
        Task<Entity?> GetByIdAsync(int entityId);
        Task<Entity> UpdateAsync(Entity entity, int entityId);
    }
}