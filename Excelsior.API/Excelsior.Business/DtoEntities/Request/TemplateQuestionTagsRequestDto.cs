using Excelsior.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Request
{
    public class TemplateQuestionTagsRequestDto : BaseRequestDto
    { 
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
