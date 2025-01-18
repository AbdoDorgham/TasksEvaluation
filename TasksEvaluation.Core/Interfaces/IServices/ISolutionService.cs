using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksEvaluation.Core.DTOs;
using TasksEvaluation.Core.Entities.Business;

namespace TasksEvaluation.Core.Interfaces.IServices
{
    public interface ISolutionService : IBaseServices<Solution , SolutionDto>
    {
    }
}
