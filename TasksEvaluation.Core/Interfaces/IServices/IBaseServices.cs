using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TasksEvaluation.Core.Interfaces.IServices
{
    public interface IBaseServices<TEntity , TDto>
    {
        Task<IEnumerable<TDto>> GetAll();
        Task<TDto> GetById(int id);
        Task<bool> Contains(TDto entityTDto);
        Task<bool> Contains(Expression<Func<TDto,bool>> crieteria);
        Task Create(TDto entityTDto);
        Task CreateRange(IEnumerable<TDto> entitiesTDto);
        void Update(TDto entityTDto);
        void Delete(int id);
        void DeleteRange(IEnumerable<TDto> entitiesTDto);
        Task<bool> Any(); 
    }
}
