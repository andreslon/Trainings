using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities.Request
{
    public class GradingResultRequestDto
    {
        public long QuestionId { get; set; }

        public string QuestionString { get; set; }

        public string AnswerString { get; set; }

        public string Laterality { get; set; }
    }
}
