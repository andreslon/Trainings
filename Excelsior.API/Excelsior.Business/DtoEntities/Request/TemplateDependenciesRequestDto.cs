using Excelsior.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Request
{
    public class TemplateDependenciesRequestDto : BaseRequestDto
    { 

        public long Id { get; set; }
        public long? SourceAnswerId { get; set; }
        public long? TargetQuestionId { get; set; }
        public long? TargetAnswerId { get; set; }
        public bool ActionEnable { get; set; }
    }
}
