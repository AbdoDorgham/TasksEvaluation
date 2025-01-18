using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskEvaluation.InfraStructure.Data;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Core.Entities.General;
using TasksEvaluation.Core.Interfaces.IRepositories;

namespace TaskEvaluation.InfraStructure.Repositories.General
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> GetAll() => _dbSet.Where(e => !e.IsDeleted);
		public async Task<T> GetById(int id) => await _dbSet.SingleOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
		public async Task<bool> Contains(T model) => await _dbSet.ContainsAsync(model);
        public async Task<bool> Contains(Expression<Func<T, bool>> crieteria) => await _dbSet.AnyAsync(crieteria);
        public async Task<int> Count() => await _dbSet.CountAsync();
        public async Task<int> Count(Expression<Func<T, bool>> crieteria) => await _dbSet.CountAsync(crieteria);
        public async Task Create(T model) => await _dbSet.AddAsync(model);
        public async Task CreateRange(IEnumerable<T> model) => await _dbSet.AddRangeAsync(model);
		public void Update(T model)
		{
			var trackedEntity = _dbSet.Local.FirstOrDefault(e => e.Id == model.Id);
			if (trackedEntity != null)
			{
				_context.Entry(trackedEntity).State = EntityState.Detached;
			}
			_context.Entry(model).State = EntityState.Modified;
			model.UpdateDate = DateTime.Now;

		}
		public void Delete(int id)
		{
			var entity = GetById(id);
			entity.Result.IsDeleted = true;
			Update(entity.Result);
		}
		public void DeleteRange(IEnumerable<T> model)
		{
			for (int i = 0; i < model.Count(); i++)
			{
				model.ElementAt(i).IsDeleted = true;
				Update(model.ElementAt(i));
			}
		}
		public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public async Task<bool> Any() => await _dbSet.AnyAsync();
        










    }
}
