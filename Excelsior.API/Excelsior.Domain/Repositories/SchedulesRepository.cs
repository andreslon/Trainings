using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class SchedulesRepository : EntityBaseRepository<PACS_TPProcList>, ISchedulesRepository
    {
        #region Constructor

        public SchedulesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<PACS_TPProcList> GetAll(long? trialId, long? timePointId, long? procedureId, long? subjectId, string search)
        {
            var entities = GetAll();

            if (trialId.HasValue)
            {
                entities = entities.Where(x => x.PACSTimePointsList.TrialID == trialId);
            }
               
            if (timePointId.HasValue)
            {
                entities = entities.Where(x => x.TimePointsListID == timePointId);
            }
            if (procedureId.HasValue)
            {
                entities = entities.Where(x => x.ImgProcedureID == procedureId);
            }

            if(subjectId.HasValue)
            {
                var exclude = Context.WF_Sequences.Where(x => x.IsActive && x.PACSTPProcList.ImgProcedureID == procedureId && x.PACSTimePoint.SubjectID == subjectId 
                    && ("[Grade][Verify][Completed]").Contains(x.WFTempStep.WFStepList.WFStepListDes)).Select(x => x.PACSTimePoint.PACSTimePointsList.TimePointsDescription);
                entities = entities.Where(x => !exclude.Contains(x.PACSTimePointsList.TimePointsDescription));
            }

            if(!string.IsNullOrEmpty(search))
            {
                entities = entities.Where(x => x.CERTImgProcedureList.ImgProcedureName.Contains(search)
                    || x.CERTImgProcedureList.ImgProcedureDescription.Contains(search)
                    || x.PACSTimePointsList.TimePointsDescription.Contains(search));
            }

            return entities;
        }

        #endregion
    }
}
