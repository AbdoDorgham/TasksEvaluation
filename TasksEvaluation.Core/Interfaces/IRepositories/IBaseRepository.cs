using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.General;

namespace TasksEvaluation.Core.Interfaces.IRepositories
{
	public interface IBaseRepository<T> where T : BaseModel
	{
        IQueryable<T> GetAll();
		Task<T> GetById(int id);
		Task<bool> Contains(T model);
		Task<bool> Contains(Expression<Func<T,bool>> crieteria);
		Task<int> Count();
		Task<int> Count(Expression<Func<T,bool>> crieteria); 
		Task Create(T model);
		Task CreateRange(IEnumerable<T> model);	
		void Update(T model);
	    void Delete(int id);
		void DeleteRange(IEnumerable<T> model);
		Task<int> SaveChangesAsync();
	    Task<bool> Any();


    }
}

