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
	public class AssignmentService : BaseServices<Assignment, AssignmentDto>, IAssignmentService
	{
		public AssignmentService(IBaseRepository<Assignment> baseRepository, IMapper mapper) : base(baseRepository, mapper)
		{
		}

		public async Task<IEnumerable<AssignmentDto>> GetAssignmentsIncludeGroups()
		{
			var assignmentsWithGroups = await _baseRepository.GetAll().Include(a => a.Group).ToListAsync();

			return _mapper.Map<IEnumerable<AssignmentDto>>(assignmentsWithGroups);
		}

		public async Task<IEnumerable<AssignmentDto>> GetAssignmentsAccordingSpecificStudent(int? groupId)
		{
			var assignmentsWithGroups = await _baseRepository.GetAll().Where(g => g.GroupId == groupId).Include(a => a.Group).Include(a => a.Solutions).ToListAsync();
			return _mapper.Map<IEnumerable<AssignmentDto>>(assignmentsWithGroups);
		}

	}
}


