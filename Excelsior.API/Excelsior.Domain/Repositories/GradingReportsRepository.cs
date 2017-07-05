using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class GradingReportsRepository : EntityBaseRepository<GRD_Report>, IReportsRepository
    {
        #region Constructor

        public GradingReportsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        public IQueryable<GRD_Report> GetAll(long? seriesId, long? performedById, bool? isActive, bool? isPrimary, bool? isSigned)
        {
            var query = GetAll();

            if(seriesId.HasValue)
            {
                query = query.Where(x => x.SeriesID == seriesId);
            }

            if (performedById.HasValue)
            {
                query = query.Where(x => x.PerformedBy == performedById);
            }

            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive);
            }

            if (isPrimary.HasValue)
            {
                query = query.Where(x => x.IsPrimaryResult == isPrimary);
            }

            if (isSigned.HasValue)
            {
                query = query.Where(x => x.IsSigned == isSigned);
            }

            return query;
        }

        public override void Delete(GRD_Report entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<GRD_Report, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }
    }
}
