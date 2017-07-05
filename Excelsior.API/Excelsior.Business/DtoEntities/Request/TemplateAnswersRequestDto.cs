using Excelsior.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Request
{
    public class TemplateAnswersRequestDto : BaseRequestDto
    { 
        public long Id { get; set; }
        public long? QuestionId { get; set; }
        public long? LibraryAnswerId { get; set; }
        public string Value { get; set; }
        public string AltString { get; set; }
        public int Index { get; set; }
    }
}
