using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Full
{
    public class MediaFullDto : MediaBaseDto
    {
        public MediaFullDto()
            : this(null)
        {
            
        }
        public MediaFullDto(PACS_RawDatum entity, object sender = null)
            : base(entity, sender)
        {
            //Frames = new List<FrameFullDto>();
            //Measurements = new List<MeasurementFullDto>();
            //ProcessedData = new List<ProcessedDataFullDto>();

            if (entity != null)
            {
                //if (!(sender is SeriesBaseDto) && entity.PACSSeries != null)
                //{
                //    Series = new SeriesFullDto(entity.PACSSeries as WF_Sequence, this);
                //}
                //if (!(sender is FrameBaseDto) && entity.PACS_DicomFrames.Count > 0)
                //{
                //    foreach (var result in entity.PACS_DicomFrames)
                //    {
                //        Frames.Add(new FrameFullDto(result, this));
                //    }
                //}
                //if (!(sender is MeasurementBaseDto) && entity.MEA_Measurements.Count > 0)
                //{
                //    foreach (var result in entity.MEA_Measurements)
                //    {
                //        Measurements.Add(new MeasurementFullDto(result, this));
                //    }
                //}
                //if (!(sender is ProcessedDataBaseDto) && entity.PACS_ProcessedData.Count > 0)
                //{
                //    foreach (var result in entity.PACS_ProcessedData)
                //    {
                //        ProcessedData.Add(new ProcessedDataFullDto(result, this));
                //    }
                //}
            }
        }
        public override PACS_RawDatum ToEntity(PACS_RawDatum entity = null, string fields = null)
        {
            entity = base.ToEntity(entity, fields);
            //entity.Series = Series.ToEntity(entity.PACSSeries);

            return entity;
        }

        //public SeriesFullDto Series { get; set; }
        //public List<FrameFullDto> Frames { get; set; }
        //public List<MeasurementFullDto> Measurements { get; set; }
        //public List<ProcessedDataFullDto> ProcessedData { get; set; } 
    }
}