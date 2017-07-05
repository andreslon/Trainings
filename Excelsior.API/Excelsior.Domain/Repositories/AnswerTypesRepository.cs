using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class AnswerTypesRepository : EntityBaseRepository<CRF_AnswerType>, IAnswerTypesRepository
    {
        #region Constructor

        public AnswerTypesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<CRF_AnswerType> GetAll(string search)
        {
            var query = GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.AnswerTypeName.Contains(search));
            }
            return query;
        }

        public override void Delete(CRF_AnswerType entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<CRF_AnswerType, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        #endregion
    }
}
