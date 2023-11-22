
using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Application.Interfaces.Repositories.Generics;
using RedSocial.Infrastructure.Persistence.Contexts;

namespace RedSocial.Infrastructure.Persistence.Repositories.Generics
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationContext _contexto;

        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationContext context)
        {
            _contexto = context;
            _dbSet = _contexto.Set<T>();
        }


        public async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<T>> GetAllWithInclude(List<string> propierties)
        {
            var query = _dbSet.AsQueryable();
            foreach (string propiedad in propierties)
            {
                query.Include(propiedad);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {

            var busqueda = await _dbSet.FindAsync(id);
            if (busqueda != null)
            {
                return busqueda;
            }

            throw new Exception("El ID INGRESADO no existe en la base de datos");
        }
        public virtual async Task<T> Add(T objeto)
        {
            await _dbSet.AddAsync(objeto);
            await _contexto.SaveChangesAsync();
            return objeto;
        }
        public async Task Update(T objeto, int id)
        {
            var entry = await _dbSet.FindAsync(id);
            _dbSet.Entry(entry).CurrentValues.SetValues(objeto);
            await _contexto.SaveChangesAsync();
        }
        public async Task Delete(T objeto)
        {
           
                _dbSet.Remove(objeto);
                await _contexto.SaveChangesAsync();
            
        }
    }
}
