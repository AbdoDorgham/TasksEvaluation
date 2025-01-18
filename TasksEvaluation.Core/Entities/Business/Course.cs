using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.General;

namespace TasksEvaluation.Core.Entities.Business
{
    public class Course : BaseModel
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }


		#region Foreign keys and navigation Properties
		public ICollection<Group> Groups { get; set; }
		#endregion


	}
}
