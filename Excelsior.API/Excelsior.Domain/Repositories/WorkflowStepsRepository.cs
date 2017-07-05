using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class WorkflowStepsRepository : EntityBaseRepository<WF_StepList>, IWorkflowStepsRepository
    {
        #region Constructor

        public WorkflowStepsRepository(DataModel context) : base(context)
        {
        }

        #endregion
    }
}
