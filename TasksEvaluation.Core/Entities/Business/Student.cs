using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.General;

namespace TasksEvaluation.Core.Entities.Business
{
    public class Student : BaseModel
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }

		#region Foreign keys and navigation Properties
		// Group Relation
		public int? GroupId { get; set; }
		public Group Group { get; set; }

		// Solution Relation
		public ICollection<Solution> Solutions { get; set; }

		#endregion


	}
}
