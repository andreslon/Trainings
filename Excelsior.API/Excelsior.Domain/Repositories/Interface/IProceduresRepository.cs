using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public interface IProceduresRepository : IEntityBaseRepository<CERT_ImgProcedureList>
    {
        IQueryable<CERT_ImgProcedureList> GetAll(string search);
    }
}
