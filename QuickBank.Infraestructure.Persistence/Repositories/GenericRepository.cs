

using QuickBank.Core.Application.Interfaces.Repositories;

namespace QuickBank.Infraestructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationContext _dbContext;
        public GenericRepository(ApplicationContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _dbContext.Set<Entity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(Entity entity)
        {
            _dbContext.Set<Entity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllAsync()
        {
            return await _dbContext.Set<Entity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<Entity>> GetAllWithIncludeAsync(IEnumerable<string> properties)
        {
            var query = _dbContext.Set<Entity>().AsQueryable();
            foreach (var property in properties)
            {
                query = query.Include(property);
            }
            return await query.ToListAsync();
        }

        public Task<List<Entity>> GetAllWithIncludeAsync(List<string> properties)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Entity>().FindAsync(id);
        }

        public virtual async Task<Entity> UpdateAsync(Entity entity, int entityId)
        {
            Entity entry = await _dbContext.Set<Entity>().FindAsync(entityId);
            _dbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            return entry;
        }
    }
}
