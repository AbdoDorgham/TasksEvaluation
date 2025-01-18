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
	public class SolutionDto : BaseDto , IMapFrom
    {
		public string SolutionFile { get; set; }
		public string Notes { get; set; }

		public int? GradeId { get; set; }
		public EvaluationGrade EvaluationGrade { get; set; }
		// Student Relation
		public int? StudentId { get; set; }
		public Student Student { get; set; }
		// Assignment Relation
		public int? AssignmentId { get; set; }
		public Assignment Assignment { get; set; }

		public void Mapping(Profile profile)
        {
            profile.CreateMap<Solution, SolutionDto>().ReverseMap();
        }
    }
}
