using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class ProceduresRepository : EntityBaseRepository<CERT_ImgProcedureList>, IProceduresRepository
    {
        #region Constructor

        public ProceduresRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<CERT_ImgProcedureList> GetAll(string search)
        {
            var query = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.ImgProcedureName.Contains(search)
                    || x.ImgProcedureDescription.Contains(search));
            }

            return query;
        }

        #endregion
    }
}
