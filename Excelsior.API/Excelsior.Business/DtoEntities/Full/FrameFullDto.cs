using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.DtoEntities.Full
{
    public class FrameFullDto : FrameBaseDto
    {
        public FrameFullDto()
            : this(null)
        {
        }
        public FrameFullDto(PACS_DicomFrame entity, object sender = null)
            : base(entity, sender)
        {
            ProcessedData = new List<ProcessedDataFullDto>();

            if (entity != null)
            {
                if (!(sender is MediaBaseDto) && entity.PACSRawDatum != null)
                {
                    Media = new MediaFullDto(entity.PACSRawDatum, this);
                }
                if (!(sender is ProcessedDataBaseDto) && entity.PACS_ProcessedData.Count > 0)
                {
                    foreach (var result in entity.PACS_ProcessedData)
                    {
                        ProcessedData.Add(new ProcessedDataFullDto(result, this));
                    }
                }
            }
        }
        public override PACS_DicomFrame ToEntity(PACS_DicomFrame entity = null, string fields = null)
        {
            entity = base.ToEntity(entity, fields);

            return entity;
        }

        public MediaFullDto Media { get; set; }
        public List<ProcessedDataFullDto> ProcessedData { get; set; }
    }
}
