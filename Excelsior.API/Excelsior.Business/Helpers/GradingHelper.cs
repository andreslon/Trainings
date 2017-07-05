using Excelsior.Business.DtoEntities.Full;
using Excelsior.Domain;
using Excelsior.Domain.Helpers;
using Excelsior.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Business.Helpers
{
    public static class GradingHelper
    {
        public static void fillTemplateResult(
            IGradingTemplatesRepository repository, 
            GRD_GradingTemplate template, 
            GradingTemplateFullDto templateResult, 
            bool isHierarchical)
        {
            if (isHierarchical)
                templateResult.Groups = new List<GradingQuestionGroupFullDto>();
            else
                templateResult.Questions = new List<GradingTemplateQuestionFullDto>();

            var tAnswers = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return repository.GetGradingAnswersForTemplate(template.GTemplateID, template.TrialID).ToList();
            });

            var tempQuestions = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return repository.GetGradingTemplateQuestionsForTemplate(template.GTemplateID).ToList();
            });

            GRD_QuestionGroup group = null;
            GradingQuestionGroupFullDto groupResponse = null;

            if (tempQuestions != null)
            {
                foreach (var tempQuestion in tempQuestions)
                {
                    var dto = new GradingTemplateQuestionFullDto(tempQuestion);
                    if (tempQuestion.GRDGradingQuestion != null)
                    {
                        dto.Question = new GradingQuestionFullDto(tempQuestion.GRDGradingQuestion);
                        dto.Question.fillAnswers(tAnswers, tempQuestions);                        
                    }

                    bool isNewGroup = false;
                    group = tempQuestion.GRDQuestionGroup;

                    if (group != null)
                    {
                        if (groupResponse == null || groupResponse.Id != group.GQuestionGroupID)
                        {
                            isNewGroup = true;
                            groupResponse = new GradingQuestionGroupFullDto(group);
                        }
                        if (isHierarchical)
                        {
                            if (isNewGroup)
                            {
                                groupResponse.Questions = new List<GradingTemplateQuestionFullDto>();
                                templateResult.Groups.Add(groupResponse);
                            }
                            groupResponse.Questions.Add(dto);
                        }
                        else
                        {
                            dto.Group = groupResponse;
                            templateResult.Questions.Add(dto);
                        }
                    }
                }
            }
        }
    }
}
