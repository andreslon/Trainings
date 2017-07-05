using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class CrfDataRepository : EntityBaseRepository<CRF_Datum>, ICrfDataRepository
    {
        #region Constructor

        public CrfDataRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<CRF_Datum> GetAll(long? seriesId, long? subjectId, long? timePointId, long? procedureId, bool? isActive)
        {
            var query = GetAll();

            if(seriesId.HasValue)
            {
                query = query.Where(x => x.SeriesID == seriesId);
            }
            if (subjectId.HasValue)
            {
                query = query.Where(x => x.PACSSeries != null && x.PACSSeries.PACSTimePoint != null && x.PACSSeries.PACSTimePoint.SubjectID == subjectId);
            }
            if (timePointId.HasValue)
            {
                query = query.Where(x => x.PACSSeries != null && x.PACSSeries.PACSTimePoint != null && x.PACSSeries.PACSTimePoint.TimePointsListID == timePointId);
            }
            if (procedureId.HasValue)
            {
                query = query.Where(x => x.PACSSeries != null && x.PACSSeries.PACSTPProcList != null && x.PACSSeries.PACSTPProcList.ImgProcedureID == procedureId);
            }
            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive);
            }

            return query;
        }

        public override void Delete(CRF_Datum entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<CRF_Datum, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        public IQueryable<CRF_DataResult> GetResults(long id)
        {
            return Context.CRF_DataResults.Where(x => x.CRFDataID == id);
        }

        public CRF_DataResult AddResult(CRF_Datum entity, CRF_DataResult result)
        {
            if (result.CRFDataResultID <= 0)
            {
                Context.Add(result);
                result.CRFDataID = entity.CRFDataID;
                result.CRFData = entity;
            }
            else
            {
                Context.AttachCopy(result);
            }

            return result;
        }

        public CRF_TemplateAnswer AddResultAnswer(CRF_DataResult entity, CRF_TemplateAnswer answer)
        {
            if (!entity.CRF_TemplateAnswers.Contains(answer))
                entity.CRF_TemplateAnswers.Add(answer);
            return answer;
        }

        #endregion
    }
}
