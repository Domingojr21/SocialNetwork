
using AutoMapper;
using RedSocial.Core.Application.Interfaces.Repositories.Generics;
using RedSocial.Core.Application.Interfaces.Services.Generics;

namespace RedSocial.Core.Application.Services.Generics
{
    public class GenericServices<SaveViewModel, ViewModel, Model> : IGenericServices<SaveViewModel, ViewModel, Model>
         where SaveViewModel : class
         where ViewModel : class
         where Model : class
    {
        private readonly IGenericRepository<Model> _repository;
        private readonly IMapper _mapper;

        public GenericServices(IGenericRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<List<ViewModel>> GetAll()
        {
            var entityList = await _repository.GetAll();

            return _mapper.Map<List<ViewModel>>(entityList);
        }

        public virtual async Task<SaveViewModel> GetById(int id)
        {
            var entity = await _repository.GetById(id);

            SaveViewModel vm = _mapper.Map<SaveViewModel>(entity);
            return vm;
        }

        public virtual async Task<SaveViewModel> Add(SaveViewModel vm)
        {
            Model entity = _mapper.Map<Model>(vm);

            entity = await _repository.Add(entity);

            SaveViewModel entityVm = _mapper.Map<SaveViewModel>(entity);

            return entityVm;
        }

        public virtual async Task Update(SaveViewModel vm, int id)
        {
            Model entity = _mapper.Map<Model>(vm);
            await _repository.Update(entity, id);
        }

        public virtual async Task Delete(int id)
        {
            var product = await _repository.GetById(id);
            await _repository.Delete(product);
        }       

    }
    

    }

