using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.General;

namespace TasksEvaluation.Core.Entities.Business
{
    public class Group : BaseModel
    {
        public string Title { get; set; }


		#region  Foreign keys and navigation Properties
		// course Relation
		public int? CourseId { get; set; }
		public Course Course { get; set; }

		// Student Relation 
		public ICollection<Student> Students { get; set; }
		// Assignment Relation
		public ICollection<Assignment> Assignments { get; set; }


		#endregion


	}
}
