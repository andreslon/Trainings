using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Infrastructure.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Base
{
    public class StudyReportBaseDto
    {
        public StudyReportBaseDto()
            : this(null)
        {

        }
        public StudyReportBaseDto(RPT_TrialReport entity, object sender = null)
        {
            if (entity != null)
            {
                Id = entity.TrialReportID;
                ReportId = entity.ReportID;
                StudyId = entity.TrialID;
                if (string.IsNullOrEmpty(entity.ReportAlias))
                    Name = entity.RPTReport.ReportName;
                else
                    Name = entity.ReportAlias;
                ClassName = entity.RPTReport.ReportAPIClass;
                CategoryName = entity.RPTReport.RPTReportCategory.ReportCategoryName;
            }
        }
        public virtual RPT_TrialReport ToEntity(RPT_TrialReport entity = null, string fields = null)
        {
            if (entity == null)
            {
                entity = new RPT_TrialReport();
            }
            entity.TrialReportID = Id.GetValueOrDefault();

            using (var fieldvalidation = new FieldValidation(fields))
            {
                if (fieldvalidation["reportid"])
                    entity.ReportID = ReportId;
                if (fieldvalidation["studyId"])
                    entity.TrialID = StudyId;
                if (fieldvalidation["name"])
                    entity.ReportAlias = Name;
            }

            return entity;
        }

        [Range(0, long.MaxValue)]
        public long? Id { get; set; }
        [Range(0, long.MaxValue)]
        public long? ReportId { get; set; }
        [Range(0, long.MaxValue)]
        public long? StudyId { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string CategoryName { get; set; }
    }
}
