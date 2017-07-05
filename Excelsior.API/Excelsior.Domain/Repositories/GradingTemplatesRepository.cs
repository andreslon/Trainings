using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class GradingTemplatesRepository : EntityBaseRepository<GRD_GradingTemplate>, IGradingTemplatesRepository
    {
        #region Constructor

        public GradingTemplatesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        public IQueryable<GRD_GradingTemplate> GetAll(long? trialId, bool? isActive, bool? isLocked, string search)
        {
            var templates = GetAll();

            if(trialId.HasValue)
            {
                templates = templates.Where(item => item.TrialID == trialId);
            }

            if (isActive.HasValue)
            {
                templates = templates.Where(item => item.IsActive == isActive);
            }

            if (isLocked.HasValue)
            {
                templates = templates.Where(item => item.IsLocked == isLocked);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                templates = templates.Where(x =>
                    x.GTemplateName.Contains(search) ||
                    x.GTemplateDes.Contains(search));
            }

            return templates;
        }

        public GRD_GradingTemplate GetGradingTemplateForProcedure(long procedureId, long timePointListId)
        {
            var tpProcList = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Context.PACS_TPProcLists
                    .SingleOrDefault(item => item.ImgProcedureID == procedureId && item.TimePointsListID == timePointListId);
            });
            return tpProcList?.GRDGradingTemplate;
        }

        public IQueryable<GRD_TempQuestion> GetGradingTemplateQuestionsForTemplate(long templateId)
        {
            return Context.GRD_TempQuestions
                .Where(x => x.GRDQuestionGroup.GTemplateID == templateId)
                .OrderBy(x => x.GRDQuestionGroup.GQuestionGroupSeq).ThenBy(x => x.GTempQuestionSeqInGroup);
        }

        public IQueryable<GRD_GradingAnswer> GetGradingAnswersForTemplate(long templateId, long? trialId)
        {
            return Context.GRD_GradingAnswers.Where(x => (x.TrialID == null || x.TrialID == trialId) 
                && x.GRDGradingQuestion.GRD_TempQuestions.Any(y => y.GRDQuestionGroup.GTemplateID == templateId));
        }

        public IQueryable<GRD_Dependency> GetGradingDependenciesForTemplate(long templateId)
        {
            return Context.GRD_Dependencies.Where(x => x.GRDGradingQuestion.GRD_TempQuestions.Any(y => y.GRDQuestionGroup.GTemplateID == templateId)
                || x.GRDGradingAnswer.GRDGradingQuestion.GRD_TempQuestions.Any(y => y.GRDQuestionGroup.GTemplateID == templateId)
                || x.GRDGradingAnswer1.GRDGradingQuestion.GRD_TempQuestions.Any(y => y.GRDQuestionGroup.GTemplateID == templateId));
        }

        public override void Delete(GRD_GradingTemplate entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<GRD_GradingTemplate, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }
    }
}
