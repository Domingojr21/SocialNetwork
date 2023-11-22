
namespace RedSocial.Core.Application.Interfaces.Services.Generics
{
    public interface IGenericServices<SaveViewModel, ViewModel, Model>
        where SaveViewModel : class
        where ViewModel : class
        where Model : class
    {
        Task Update(SaveViewModel vm, int id);

        Task<SaveViewModel> Add(SaveViewModel vm);

        Task Delete(int id);

        Task<SaveViewModel> GetById(int id);

        Task<List<ViewModel>> GetAll();
    }
}
