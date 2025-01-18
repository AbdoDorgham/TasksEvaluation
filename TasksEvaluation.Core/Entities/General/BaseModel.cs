using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksEvaluation.Core.Entities.General
{
    abstract public class BaseModel
    {
        public int Id { get; set; }
        public DateTime? EntryDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
