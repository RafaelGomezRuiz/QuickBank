namespace QuickBank.Core.Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel, Entity>
        where SaveViewModel : class
        where ViewModel : class
        where Entity : class
    {
        Task<SaveViewModel?> AddAsync(SaveViewModel svm);
        Task DeleteAsync(int entityId);
        Task<List<ViewModel>> GetAllAsync();
        Task<SaveViewModel> GetByIdAsync(int entityId);
        Task<SaveViewModel?> UpdateAsync(SaveViewModel svm, int svmId);
    }
}