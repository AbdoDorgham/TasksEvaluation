using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Entities.General;
using TasksEvaluation.Core.Interfaces.IRepositories;
using TasksEvaluation.Core.Interfaces.IServices;

namespace TaskEvaluation.InfraStructure.Repositories.Services
{
    public class BaseServices<TEntity , TDto> : IBaseServices<TEntity, TDto> where TEntity : BaseModel  where TDto : BaseDto
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        public BaseServices(IBaseRepository<TEntity> baseRepository , IMapper mapper) 
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
                    
        }
        public async Task<IEnumerable<TDto>> GetAll()
        {
            var listEntities = await  _baseRepository.GetAll().ToListAsync();
            return _mapper.Map<IEnumerable<TDto>>(listEntities);
        }
        public async Task<TDto> GetById(int id)
        {
            var entity =await _baseRepository.GetById(id);
            return _mapper.Map<TDto>(entity);
        }
        public async Task<bool> Contains(TDto entityTDto) => await _baseRepository.Contains(_mapper.Map<TEntity>(entityTDto));
        public async Task<bool> Contains(Expression<Func<TDto, bool>> crieteria) =>
            await _baseRepository.Contains(_mapper.Map<Expression<Func<TEntity, bool>>>(crieteria));
        public async Task Create(TDto entityTDto)
        {
            var entity = _mapper.Map<TEntity>(entityTDto);
            await _baseRepository.Create(entity);
        }
        public async Task CreateRange(IEnumerable<TDto> entitiesTDto)
        {
            var listEntities = _mapper.Map<IEnumerable<TEntity>>(entitiesTDto);
            await _baseRepository.CreateRange(listEntities);
        }
        public void Update(TDto entityTDto) => _baseRepository.Update(_mapper.Map<TEntity>(entityTDto));

        public void Delete(int id) => _baseRepository.Delete(id);
        public void DeleteRange(IEnumerable<TDto> entitiesTDto) => _baseRepository.DeleteRange(_mapper.Map<IEnumerable<TEntity>>(entitiesTDto));
        public async Task<bool> Any() => await _baseRepository.Any();
    }
}
