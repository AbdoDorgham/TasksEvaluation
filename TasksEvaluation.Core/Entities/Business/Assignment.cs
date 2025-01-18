using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.General;

namespace TasksEvaluation.Core.Entities.Business
{

    public class Assignment : BaseModel
    {
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime? Deadline { get; set; }

		#region Foreign keys and navigation Properties
		// Group Relation
		public int? GroupId { get; set; }
        public Group Group { get; set; }

        // Solution Relation
        public ICollection<Solution> Solutions { get; set; }
		#endregion
	}
}
