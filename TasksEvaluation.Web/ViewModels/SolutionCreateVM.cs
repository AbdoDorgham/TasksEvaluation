using TasksEvaluation.Core.Entities.Business;

namespace TasksEvaluation.Web.ViewModels
{
	public class SolutionCreateVM
	{
		public int Id { get; set; }
		public IFormFile SolutionFile { get; set; }
		public string Notes { get; set; }

		public int? GradeId { get; set; }
		public EvaluationGrade EvaluationGrade { get; set; }
		public int? StudentId { get; set; }
		public Student Student { get; set; }
		public int? AssignmentId { get; set; }
		public Assignment Assignment { get; set; }
	}
}
