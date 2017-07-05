using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class TimePointBaseDto
    {
        public TimePointBaseDto()
            : this(null)
        {
        }
        public TimePointBaseDto(PACS_TimePointsList entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.TimePointsListID;
                StudyId = entity.TrialID;
                Description = entity.TimePointsDescription;
                Index = entity.TimePointsSeq;
                IsInitial = entity.IsInitialTimePoint;
                IsTerminal = entity.IsTerminalTimePoint;
                IsEligibility = entity.IsEligibilityTimePoint;
                ExpectedVisitStartDay = entity.ExpectedVisitStartDay;
                ExpectedVisitEndDay = entity.ExpectedVisitEndDay;
            }
        }
        public virtual PACS_TimePointsList ToEntity(PACS_TimePointsList entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new PACS_TimePointsList();
            }

            entity.TimePointsListID = Id.GetValueOrDefault();
            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["studyid"])
                    entity.TrialID = StudyId;
                if (fieldvalidation["description"])
                    entity.TimePointsDescription = Description;
                if (fieldvalidation["index"])
                    entity.TimePointsSeq = Index;
                if (fieldvalidation["isinitial"])
                    entity.IsInitialTimePoint = IsInitial.GetValueOrDefault();
                if (fieldvalidation["isterminal"])
                    entity.IsTerminalTimePoint = IsTerminal.GetValueOrDefault();
                if (fieldvalidation["iseligibility"])
                    entity.IsEligibilityTimePoint = IsEligibility.GetValueOrDefault();
                if (fieldvalidation["expectedvisitstartday"])
                    entity.ExpectedVisitStartDay = ExpectedVisitStartDay;
                if (fieldvalidation["expectedvisitendDay"])
                    entity.ExpectedVisitEndDay = ExpectedVisitEndDay;

            }


            return entity;
        }
        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public int? Index { get; set; }
        public bool? IsInitial { get; set; }
        public bool? IsTerminal { get; set; }
        public bool? IsEligibility { get; set; }
        [Range(0, int.MaxValue)]
        public int? ExpectedVisitStartDay { get; set; }
        [Range(0, int.MaxValue)]
        public int? ExpectedVisitEndDay { get; set; }
    }
}
