using AutoMapper;
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
    public class CourseService : BaseServices<Course, CourseDto>, ICourseService
    {
        public CourseService(IBaseRepository<Course> baseRepository, IMapper mapper) : base(baseRepository, mapper)
        {
        }
    }
}
