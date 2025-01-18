using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.Entities.General;

namespace TasksEvaluation.Core.Entities.Business
{
    public class EvaluationGrade : BaseModel
    {
        public string Grade { get; set; }

        #region Foreign keys and navigation Properties
        // Solution Relation
        public ICollection<Solution> Solutions { get; set; }
        #endregion




    }
}
