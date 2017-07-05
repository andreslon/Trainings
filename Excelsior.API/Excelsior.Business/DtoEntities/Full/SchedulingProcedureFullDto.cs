using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Full
{

    public class SchedulingProcedureFullDto : ProcedureBaseDto
    {
        public SchedulingProcedureFullDto()
            : this(null)
        {
        }
        public SchedulingProcedureFullDto(CERT_ImgProcedureList entity, object sender = null)
            : base(entity, sender)
        {
            Schedules = new List<SchedulingScheduleFullDto>();

            if (entity != null)
            {
                //TODO:
                //if (!(sender is LibraryDependencyBaseDto) && entity.PACS_TPProcLists.Count > 0)
                //{
                //    foreach (var lde in entity.PACS_TPProcLists)
                //    {
                //        TimePoints.Add(new VisitMatrixTimePointFullDto(lde, this));
                //    }
                //}
            }
        }
        public override CERT_ImgProcedureList ToEntity(CERT_ImgProcedureList entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            //TODO:
            //if (TimePoints.Count > 0)
            //{
            //    entity.PACS_TPProcLists.Clear();
            //    foreach (var a in TimePoints)
            //    {
            //        var lde = a.ToEntity();
            //        entity.PACS_TPProcLists.Add(lde);
            //    }
            //}

            return entity;
        }

        public List<SchedulingScheduleFullDto> Schedules { get; set; }
    }
}
