
namespace RedSocial.Core.Application.Interfaces.Repositories.Generics
{
    public interface IGenericRepository<T>
    {
        Task<List<T>> GetAll();

        Task<List<T>> GetAllWithInclude(List<string> propierties);

        Task<T> GetById(int id);

        Task<T> Add(T objeto);

        Task Update(T objeto, int id);

        Task Delete(T objeto);
    }
}
