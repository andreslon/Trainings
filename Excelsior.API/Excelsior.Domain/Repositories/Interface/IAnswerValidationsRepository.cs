using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{ 
    public interface IAnswerValidationsRepository : IEntityBaseRepository<CRF_AnswerValidation>
    {
        IQueryable<CRF_AnswerValidation> GetAll(string search);
    }
}
