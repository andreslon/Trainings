using Excelsior.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class VisitMatrixVisitBaseDto
    {
        public VisitMatrixVisitBaseDto()
            : this(null)
        {
        }
        public VisitMatrixVisitBaseDto(PACS_TimePointsList entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.TimePointsListID;
                Description = entity.TimePointsDescription;
                Index = entity.TimePointsSeq;
                ExpectedVisitStartDay = entity.ExpectedVisitStartDay;
                ExpectedVisitEndDay = entity.ExpectedVisitEndDay;
                IsEligibility = entity.IsEligibilityTimePoint;
                IsInitial = entity.IsInitialTimePoint;
                IsTerminal = entity.IsTerminalTimePoint;
            }
        }
        public virtual PACS_TimePointsList ToEntity(PACS_TimePointsList entity = null)
        {
            if (entity == null)
            {
                entity = new PACS_TimePointsList();
            }

            entity.TimePointsListID = Id.GetValueOrDefault();
            entity.TimePointsDescription = Description;
            entity.TimePointsSeq = Index;
            entity.ExpectedVisitStartDay = ExpectedVisitStartDay;
            entity.ExpectedVisitEndDay = ExpectedVisitEndDay;
            entity.IsEligibilityTimePoint = IsEligibility.GetValueOrDefault();
            entity.IsInitialTimePoint = IsInitial.GetValueOrDefault();
            entity.IsTerminalTimePoint = IsTerminal.GetValueOrDefault(); 

            return entity;
        }

        public long? Id { get; set; }
        [StringLength(100)]
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
        [Range(0, long.MaxValue)]
        public long? SeriesId { get; set; }
        public string Status { get; set; }
        public DateTime? StudyDate { get; set; }
        public int totalQueriesPending { get; set; }
        public int totalQueriesFlagged { get; set; }
    }
}
