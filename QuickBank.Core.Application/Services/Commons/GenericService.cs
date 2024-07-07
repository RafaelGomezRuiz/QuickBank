using AutoMapper;
using QuickBank.Core.Application.Interfaces.Repositories;
using QuickBank.Core.Application.Interfaces.Services.Commons;


namespace QuickBank.Core.Application.Services.Commons
{
    public class GenericService<SaveViewModel, ViewModel, Entity> : IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        private readonly IGenericRepository<Entity> repository;
        private readonly IMapper mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public virtual async Task<SaveViewModel?> AddAsync(SaveViewModel svm)
        {
            Entity entity = await repository.AddAsync(mapper.Map<Entity>(svm));
            return mapper.Map<SaveViewModel>(entity);
        }

        public virtual async Task<SaveViewModel?> UpdateAsync(SaveViewModel svm, int svmId)
        {
            Entity entity = await repository.UpdateAsync(mapper.Map<Entity>(svm), svmId);
            return mapper.Map<SaveViewModel>(entity);
        }

        public virtual async Task DeleteAsync(int entityId)
        {
            Entity entity = await repository.GetByIdAsync(entityId);
            await repository.DeleteAsync(entity);
        }

        public virtual async Task<List<ViewModel>> GetAllAsync()
        {
            return mapper.Map<List<ViewModel>>(await repository.GetAllAsync());
        }

        public virtual async Task<SaveViewModel> GetByIdAsync(int entityId)
        {
            return mapper.Map<SaveViewModel>(await repository.GetByIdAsync(entityId));
        }
    }
}
