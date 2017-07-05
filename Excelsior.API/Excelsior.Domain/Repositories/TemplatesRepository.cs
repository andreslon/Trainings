using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class TemplatesRepository : EntityBaseRepository<CRF_Template>, ITemplatesRepository
    {
        #region Constructor

        public TemplatesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<CRF_Template> GetAll(long? trialId, long? timePointId, long? procedureId, bool? isActive, bool? isLocked, string search)
        {
            var entities = GetAll();

            if (trialId.HasValue)
            {
                entities = entities.Where(x => x.TrialID == trialId);
            }

            if (timePointId.HasValue)
            {
                entities = entities.Where(x => x.PACS_TPProcLists.Any(y => y.TimePointsListID == timePointId));
            }

            if (procedureId.HasValue)
            {
                entities = entities.Where(x => x.PACS_TPProcLists.Any(y => y.ImgProcedureID == procedureId));
            }

            if (isActive.HasValue)
            {
                entities = entities.Where(x => x.IsActive == isActive);
            }

            if (isLocked.HasValue)
            {
                entities = entities.Where(x => x.IsLocked == isLocked);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                entities = entities.Where(x => x.TemplateAbbrev.Contains(search)
                    || x.TemplateDes.Contains(search)
                    || x.TemplateName.Contains(search));
            }

            return entities;
        }

        public override void Delete(CRF_Template entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<CRF_Template, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        public IQueryable<CRF_TemplateGroup> GetGroups(long id)
        {
            return Context.CRF_TemplateGroups.Where(x => x.CRFTemplateID == id);
        }

        public CRF_TemplateGroup AddGroup(CRF_Template entity, CRF_TemplateGroup group)
        {
            if (group.CRFTemplateGroupID <= 0)
            {
                Context.Add(group);
                group.CRFTemplateID = entity.CRFTemplateID;
                group.CRFTemplate = entity;

            }
            else
            {
                Context.AttachCopy(group);
            }

            return group;
        }

        public CRF_Template Clone(CRF_Template entity, long? trialId)
        {
            var name = entity.TemplateName;
            if (trialId == null)
                name = string.Format("{0}_shared", name);
            else
                name = string.Format("{0}_cloned", name);

            //Find what name to use
            var existing = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return FindBy(x => x.TrialID == trialId && x.TemplateName.StartsWith(name)).ToList();
            });

            var exists = true;
            var i = 1;
            while (exists)
            {
                exists = existing.Any(x => x.TemplateName.ToLower() == name.ToLower());
                if (exists)
                {
                    name = string.Format("{0}_{1}", name, i);
                    i++;
                }
            }

            var cloneTemplate = new CRF_Template();
            cloneTemplate.TrialID = trialId;
            cloneTemplate.TemplateName = name;
            cloneTemplate.TemplateDes = entity.TemplateDes;
            cloneTemplate.TemplateAbbrev = entity.TemplateAbbrev;
            cloneTemplate.TemplateVersion = entity.TemplateVersion;
            cloneTemplate.AssocProtocol = entity.AssocProtocol;
            cloneTemplate.IsActive = true;
            cloneTemplate.IsLocked = false;

            Add(cloneTemplate);

            //clone Groups
            var groups = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                return Context.CRF_TemplateGroups.Where(x => x.CRFTemplateID == entity.CRFTemplateID).ToList();
            });

            foreach (var group in groups)
            {
                var cloneGroup = new CRF_TemplateGroup();
                cloneGroup.GroupName = group.GroupName;
                cloneGroup.GroupSeq = group.GroupSeq;
                Context.Add(cloneGroup);
                cloneGroup.CRFTemplate = cloneTemplate;

                //CloneQuestions
                Dictionary<long?, CRF_TemplateQuestion> qMap = new Dictionary<long?, CRF_TemplateQuestion>();
                Dictionary<long?, CRF_TemplateAnswer> aMap = new Dictionary<long?, CRF_TemplateAnswer>();

                foreach (var question in group.CRF_TemplateQuestions)
                {
                    var cloneQuestion = new CRF_TemplateQuestion();
                    cloneQuestion.CDashVariable = question.CDashVariable;
                    cloneQuestion.CRFAnswerTypeID = question.CRFAnswerTypeID;
                    cloneQuestion.CRFAnswerValidationID = question.CRFAnswerValidationID;
                    cloneQuestion.IsActive = question.IsActive;
                    cloneQuestion.IsLaterality = question.IsLaterality;
                    cloneQuestion.QuestionDes = question.QuestionDes;
                    cloneQuestion.QuestionSeq = question.QuestionSeq;
                    cloneQuestion.QuestionText = question.QuestionText;
                    cloneQuestion.SDTMVariable = question.SDTMVariable;
                    Context.Add(cloneQuestion);
                    cloneQuestion.CRFTemplateGroup = cloneGroup;

                    foreach(var tag in question.CRF_TemplateQuestionTags)
                    {
                        cloneQuestion.CRF_TemplateQuestionTags.Add(tag);
                    }

                    qMap.Add(question.CRFTemplateQuestionID, cloneQuestion);

                    foreach(var answer in question.CRF_TemplateAnswers)
                    {
                        var cloneAnswer = new CRF_TemplateAnswer();
                        cloneAnswer.AltAnswerString = answer.AltAnswerString;
                        cloneAnswer.AnswerSeq = answer.AnswerSeq;
                        cloneAnswer.AnswerString = answer.AnswerString;
                        cloneAnswer.IsActive = answer.IsActive;
                        Context.Add(cloneAnswer);
                        cloneAnswer.CRFTemplateQuestion = cloneQuestion;

                        aMap.Add(answer.CRFTemplateAnswerID, cloneAnswer);
                    }
                }

                foreach(var dependency in group.CRF_TemplateDependencies)
                {
                    var cloneDependency = new CRF_TemplateDependency();
                    cloneDependency.ActionEnable = dependency.ActionEnable;
                    cloneDependency.Expression = dependency.Expression;
                    Context.Add(cloneDependency);
                    cloneDependency.CRFTemplateGroup = cloneGroup;
                    cloneDependency.TargetAnswer = aMap[dependency.TargetAnswerID];
                    cloneDependency.TargetQuestion = qMap[dependency.TargetQuestionID];

                    foreach(var source in dependency.CRF_TemplateDependencySources)
                    {
                        var cloneSource = new CRF_TemplateDependencySource();
                        Context.Add(cloneSource);
                        cloneSource.CRFTemplateDependency = cloneDependency;
                        cloneSource.SourceAnswer = aMap[source.SourceAnswerID];
                        cloneSource.SourceQuestion = qMap[source.SourceQuestionID];
                    }
                }
            }

            return cloneTemplate;
        }

        #endregion
    }
}