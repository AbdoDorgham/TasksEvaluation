using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.Business;
using TasksEvaluation.Core.Mapper;

namespace TasksEvaluation.Core.DTOs
{
	public class AssignmentDto : BaseDto , IMapFrom
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime? Deadline { get; set; }

		public int? GroupId { get; set; }
		public Group Group { get; set; }
		public ICollection<Solution> Solutions { get; set; }

		public void Mapping(Profile profile)
        {
            profile.CreateMap<Assignment, AssignmentDto>().ReverseMap();
        }
    }
}
