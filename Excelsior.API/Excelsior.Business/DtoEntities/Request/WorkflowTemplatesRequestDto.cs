using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Request
{
    public class WorkflowTemplatesRequestDto : BaseRequestDto
    {
        public long? StudyId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
    }
}
