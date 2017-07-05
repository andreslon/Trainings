using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Full
{
    public class GradingReportFullDto: GradingReportBaseDto
    {

        public GradingReportFullDto()
            : this(null)
        {
        }
        public GradingReportFullDto(GRD_Report entity, object sender = null)
            : base(entity, sender)
        {
            Results = new List<GradingResultFullDto>();
            Measurements = new List<MeasurementBaseDto>();
            Attachments = new List<AttachmentFullDto>();

            if (entity != null)
            {
                if (!(sender is GradingResultBaseDto) && entity.GRD_ReportResults.Count > 0)
                {
                    foreach (var result in entity.GRD_ReportResults)
                    {
                        Results.Add(new GradingResultFullDto(result));
                    }
                }
                if (!(sender is MeasurementBaseDto) && entity.MEA_Measurements.Count > 0)
                {
                    foreach (var measurement in entity.MEA_Measurements)
                    {
                        Measurements.Add(new MeasurementBaseDto(measurement));
                    }
                }
                if (!(sender is AttachmentBaseDto) && entity.PACS_SeriesAttachments.Count > 0)
                {
                    foreach (var attm in entity.PACS_SeriesAttachments)
                    {
                        Attachments.Add(new AttachmentFullDto(attm));
                    }
                }
            }
        }
        public override GRD_Report ToEntity(GRD_Report entity = null, string fields = null)
        {
            entity = base.ToEntity(entity, fields);

            return entity;
        }

        public List<GradingResultFullDto> Results { get; set; }
        public List<MeasurementBaseDto> Measurements { get; set; }
        public List<AttachmentFullDto> Attachments { get; set; }
    }
}