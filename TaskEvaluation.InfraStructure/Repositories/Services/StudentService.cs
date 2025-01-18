using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Core.Interfaces.IRepositories;
using TasksEvaluation.Core.Interfaces.IServices;

namespace TaskEvaluation.InfraStructure.Repositories.Services
{
    public class StudentService : BaseServices<Student, StudentDto>, IStudentService
    {
        public StudentService(IBaseRepository<Student> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
        }



		public async Task<IEnumerable<StudentDto>> GetStudentsIncludeGroups()
        {
            var listEntities = await _baseRepository.GetAll().Include(s => s.Group).ToListAsync();
            
            return _mapper.Map<IEnumerable<StudentDto>>(listEntities); 
        }


	}
}
