using Excelsior.Business.DtoEntities.Base;
using Excelsior.Domain;
using System.ComponentModel.DataAnnotations;

namespace Excelsior.Business.DtoEntities.Full
{
    public class AnswerValidationFullDto : AnswerValidationBaseDto
    {
        public AnswerValidationFullDto()
            : this(null)
        {
        }
        public AnswerValidationFullDto(CRF_AnswerValidation entity, object sender = null)
            : base(entity, sender)
        {
            if (entity != null)
            {
                Description = entity.Description;
                Control = entity.Control;
                Mask = entity.Mask;
                Max = entity.Max;
                Min = entity.Min;
                Tick = entity.Tick;
                ToolTip = entity.ToolTip;
                Unit = entity.Unit;
            }
        }
        public override CRF_AnswerValidation ToEntity(CRF_AnswerValidation entity = null, string fields=null)
        {
            entity = base.ToEntity(entity,fields);
            entity.Description = Description;
            entity.Control = Control;
            entity.Mask = Mask;
            entity.Max = Max;
            entity.Min = Min;
            entity.Tick = Tick;
            entity.ToolTip = ToolTip;
            entity.Unit = Unit;

            return entity;
        }

        public string Description { get; set; }
        [StringLength(50)]
        public string Control { get; set; }
        public string Mask { get; set; }
        public string Max { get; set; }
        public string Min { get; set; }
        public string Tick { get; set; }
        public string ToolTip { get; set; }
        [StringLength(50)]
        public string Unit { get; set; }

        public StudyFullDto Study  { get; set; }
    }
}
