using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class TemplateQuestionsRepository : EntityBaseRepository<CRF_TemplateQuestion>, ITemplateQuestionsRepository
    {
        #region Constructor

        public TemplateQuestionsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<CRF_TemplateQuestion> GetAll(long? templateId, bool? isActive, string search)
        {
            var questions = GetAll();
            if (templateId == null)
                questions = questions.Where(x => x.CRFTemplateGroup == null);
            else
                questions = questions.Where(x => x.CRFTemplateGroup.CRFTemplateID == templateId);

            if (isActive.HasValue)
            {
                questions = questions.Where(x => x.IsActive == isActive);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                questions = questions.Where(x =>
                    x.CDashVariable.Contains(search)
                    || x.QuestionDes.Contains(search)
                    || x.QuestionText.Contains(search)
                    || x.SDTMVariable.Contains(search)
                    || ((x.CRFAnswerType != null) ? x.CRFAnswerType.AnswerTypeName.Contains(search) : false)
                    || x.CRF_TemplateQuestionTags.Any(y => y.QuestionTagName.Contains(search)));
            }

            return questions;
        }

        public override void Delete(CRF_TemplateQuestion entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<CRF_TemplateQuestion, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        public IQueryable<CRF_TemplateAnswer> GetAnswers(long id)
        {
            return Context.CRF_TemplateAnswers.Where(x => x.CRFTemplateQuestionID == id);
        }

        public CRF_TemplateAnswer AddAnswer(CRF_TemplateQuestion entity, CRF_TemplateAnswer answer)
        {
            if (answer.CRFTemplateAnswerID <= 0)
            {
                Context.Add(answer);
                answer.CRFTemplateQuestionID = entity.CRFTemplateQuestionID;
                answer.CRFTemplateQuestion = entity;

            }
            else
            {
                Context.AttachCopy(answer);
            }

            return answer;
        }

        public IQueryable<CRF_TemplateQuestionTag> GetTags(long id)
        {
            return Context.CRF_TemplateQuestionTags.Where(x => x.CRF_TemplateQuestions.Any(y => y.CRFTemplateQuestionID == id));
        }

        public CRF_TemplateQuestionTag AddTag(CRF_TemplateQuestion entity, CRF_TemplateQuestionTag tag)
        {
            if (tag.CRFTemplateQuestionTagID <= 0)
                Context.Add(tag);

            if (!entity.CRF_TemplateQuestionTags.Contains(tag))
                entity.CRF_TemplateQuestionTags.Add(tag);

            return tag;
        }

        public CRF_AnswerValidation SetValidation(CRF_TemplateQuestion entity, CRF_AnswerValidation validation)
        {
            if (validation != null)
            {
                if (validation.CRFAnswerValidationID <= 0)
                {
                    Context.Add(validation);
                    entity.CRFAnswerValidation = validation;
                }
                else
                {
                    Context.AttachCopy(validation);
                    entity.CRFAnswerValidationID = validation.CRFAnswerValidationID;
                }
            }
            else
            {
                entity.CRFAnswerValidationID = null;
            }

            return validation;
        }

        public CRF_TemplateQuestion Clone(CRF_TemplateQuestion entity, long? groupId)
        {
            //Ask Alex about using groups in the question library with TemplateId = null
            //That way it would allow multiple questions with the same Text in different library groups

            var text = entity.QuestionText;

            //Find what name to use
            var exists = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Any(x => x.CRFTemplateGroupID == groupId && x.QuestionText.ToLower() == text.ToLower());
            });

            if(exists)
            {
                var error = "A question with same text is already in the group.";
                if (groupId == null)
                    error = "A question with the same text is already in the library";
                throw new Exception(error);
            }

            int index = 1;
            if (groupId.HasValue)
            {
                index = Count(x => x.CRFTemplateGroupID == groupId && x.IsActive) + 1;
            }

            var cloneQuestion = new CRF_TemplateQuestion();

            //Clone Template fields
            cloneQuestion.CRFTemplateGroupID = groupId;
            cloneQuestion.QuestionText = text;
            cloneQuestion.QuestionSeq = index;
            cloneQuestion.IsActive = true;
            cloneQuestion.QuestionDes = entity.QuestionDes;
            cloneQuestion.SDTMVariable = entity.SDTMVariable;
            cloneQuestion.IsLaterality = entity.IsLaterality;
            cloneQuestion.CRFAnswerTypeID = entity.CRFAnswerTypeID;
            cloneQuestion.CRFAnswerValidationID = entity.CRFAnswerValidationID;

            //Tags
            foreach (var tag in entity.CRF_TemplateQuestionTags)
            {
                cloneQuestion.CRF_TemplateQuestionTags.Add(tag);
            }

            Add(cloneQuestion);

            return cloneQuestion;
        }

        #endregion
    }
}