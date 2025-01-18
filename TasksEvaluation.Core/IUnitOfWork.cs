using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksEvaluation.Core
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Complete();
    }
}
