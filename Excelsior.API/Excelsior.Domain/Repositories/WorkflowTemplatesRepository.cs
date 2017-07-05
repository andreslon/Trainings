using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class WorkflowTemplatesRepository : EntityBaseRepository<WF_Template>, IWorkflowTemplatesRepository
    {
        #region Constructor

        public WorkflowTemplatesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<WF_Template> GetAll(long? trialId, bool? isActive, bool? isLocked, string search)
        {
            var templates = GetAll();

            if (trialId.HasValue)
            {
                templates = templates.Where(x => x.TrialID == trialId);
            }

            if (isActive.HasValue)
            {
                templates = templates.Where(x => x.IsActive == isActive);
            }

            if (isLocked.HasValue)
            {
                templates = templates.Where(x => x.IsLocked == isLocked);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                templates = templates.Where(x => 
                    x.WFTemplateName.Contains(search) ||
                    x.WFTemplateNote.Contains(search) ||
                    x.WFTemplateType.Contains(search));
            }

            return templates;
        }

        public override void Delete(WF_Template entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<WF_Template, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        public IQueryable<WF_TempStep> GetSteps(WF_Template entity)
        {
            return Context.WF_TempSteps.Where(x => x.WFTemplateID == entity.WFTemplateID);
        }

        public WF_TempStep AddStep(WF_Template entity, WF_TempStep step)
        {
            if (step.WFTempStepID <= 0)
            {
                Context.Add(step);
                step.WFTemplateID = entity.WFTemplateID;
                step.WFTemplate = entity;
            }
            else
            {
                Context.AttachCopy(step);
            }

            return step;
        }

        public WF_Template Clone(WF_Template entity, long? trialId)
        {
            var name = entity.WFTemplateName;
            if (trialId == null)
                name = string.Format("{0}_shared", name);
            else
                name = string.Format("{0}_cloned", name);

            //Find what name to use
            var existing = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return FindBy(x => x.TrialID == trialId && x.WFTemplateName.StartsWith(name)).ToList();
            });

            var exists = true;
            var i = 1;
            while(exists)
            {
                exists = existing.Any(x => x.WFTemplateName.ToLower() == name.ToLower());
                if(exists)
                {
                    name = string.Format("{0}_{1}", name, i);
                    i++;
                }
            }

            var cloneTemplate = new WF_Template();

            //Clone Template fields
            cloneTemplate.TrialID = trialId;
            cloneTemplate.WFTemplateName = name;
            cloneTemplate.WFTemplateNote = entity.WFTemplateNote;
            cloneTemplate.WFTemplateType = entity.WFTemplateType;
            cloneTemplate.IsActive = true;
            cloneTemplate.IsLocked = false;

            Add(cloneTemplate);

            //clone Steps
            var steps = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Context.WF_TempSteps.Where(x => x.WFTemplateID == entity.WFTemplateID).ToList();
            });
            
            foreach(var step in steps)
            {
                var cloneStep = new WF_TempStep();

                cloneStep.ShouldSkip = step.ShouldSkip;
                cloneStep.WFStepListID = step.WFStepListID;
                cloneStep.WFStepOrder = step.WFStepOrder;
                Context.Add(cloneStep);
                cloneStep.WFTemplate = cloneTemplate;
            }

            return cloneTemplate;
        }

        #endregion
    }
}
