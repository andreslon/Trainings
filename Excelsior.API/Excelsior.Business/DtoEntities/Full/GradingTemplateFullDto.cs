using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.Collections.Generic;

namespace Excelsior.Business.DtoEntities.Full
{
    public class GradingTemplateFullDto : GradingTemplateBaseDto
    {
        public GradingTemplateFullDto()
            : this(null)
        {
        }
        public GradingTemplateFullDto(GRD_GradingTemplate entity, object sender = null)
            : base(entity, sender)
        {
            Groups = new List<GradingQuestionGroupFullDto>();
            Questions = new List<GradingTemplateQuestionFullDto>();

            if (entity != null)
            {
                //TODO:
                //if (entity.PACS_SeriesGroups.Count > 0)
                //{
                //    foreach (var lde in entity.PACS_SeriesGroups)
                //    {
                //        Questions.Add(new GradingTemplateQuestionFullDto(lde, this));
                //    }
                //}
            }
        }
        public override GRD_GradingTemplate ToEntity(GRD_GradingTemplate entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);

            //if (Groups.Count > 0)
            //{
            //    entity.GRD_QuestionGroups.Clear();
            //    foreach (var a in Groups)
            //    {
            //        var lde = a.ToEntity();
            //        entity.GRD_QuestionGroups.Add(lde);
            //    }
            //}

            return entity;
        }

        public List<GradingQuestionGroupFullDto> Groups { get; set; }
        public List<GradingTemplateQuestionFullDto> Questions { get; set; }
    }
}
