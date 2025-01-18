using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.General;

namespace TasksEvaluation.Core.Entities.Business
{

    public class Solution : BaseModel
    {
        public string SolutionFile { get; set; } 
        public string Notes { get; set; }

		// foreign Keys 

		#region Foreign keys and navigation Properties

		// EvaluationGrade Relation
		public int? GradeId { get; set; }
		public EvaluationGrade EvaluationGrade { get; set; }
		// Student Relation
		public int? StudentId { get; set; }
		public Student Student { get; set; }
		// Assignment Relation
		public int? AssignmentId { get; set; }
		public Assignment Assignment { get; set; }

		#endregion

	}
}
