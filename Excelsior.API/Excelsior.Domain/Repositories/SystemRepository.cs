using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class SystemRepository : EntityBaseRepository<EXCELSIOR_SYSTEM>, ISystemRepository
    {
        #region Constructor

        public SystemRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<EXCELSIOR_SYSTEM> GetAll(string search)
        {
            var query = GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                //query = query.Where();
            }
            return query;
        }

        public override void Add(EXCELSIOR_SYSTEM entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(EXCELSIOR_SYSTEM entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(EXCELSIOR_SYSTEM entity)
        {
            throw new NotImplementedException();
        }

        public override void DeleteWhere(Expression<Func<EXCELSIOR_SYSTEM, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}