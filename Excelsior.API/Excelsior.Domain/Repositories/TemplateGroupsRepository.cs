using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class TemplateGroupsRepository : EntityBaseRepository<CRF_TemplateGroup>, ITemplateGroupsRepository
    {
        #region Constructor

        public TemplateGroupsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions
      
        public IQueryable<CRF_TemplateGroup> GetAll(string search)
        {
            var query = GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.GroupName.Contains(search));
            }
            return query;
        }

        public IQueryable<CRF_TemplateQuestion> GetQuestions(long id)
        {
            return Context.CRF_TemplateQuestions.Where(x => x.CRFTemplateGroupID == id);
        }

        public void SetQuestions(CRF_TemplateGroup entity, IList<CRF_TemplateQuestion> questions)
        {
            foreach (var question in entity.CRF_TemplateQuestions)
            {
                if (!questions.Any(x => x.CRFTemplateQuestionID == question.CRFTemplateQuestionID))
                    Context.Delete(question);
            }
            foreach (var question in questions)
            {
                if (question.CRFTemplateQuestionID <= 0)
                {
                    Context.Add(question);
                    question.CRFTemplateGroupID = entity.CRFTemplateGroupID;
                    question.CRFTemplateGroup = entity;
                }
                else
                {
                    Context.AttachCopy(question);
                }
            }
        }

        public CRF_TemplateQuestion AddQuestion(CRF_TemplateGroup entity, CRF_TemplateQuestion question)
        {
            if (question.CRFTemplateQuestionID <= 0)
            {
                Context.Add(question);
                question.CRFTemplateGroupID = entity.CRFTemplateGroupID;
                question.CRFTemplateGroup = entity;
            }
            else
            {
                Context.AttachCopy(question);
            }

            return question;
        }

        public IQueryable<CRF_TemplateDependency> GetDependencies(long id)
        {
            return Context.CRF_TemplateDependencies.Where(x => x.CRFTemplateGroupID == id);
        }

        public void ClearExistingDependencies(CRF_TemplateGroup entity, IList<CRF_TemplateDependency> dependencies)
        {
            foreach (var dependency in entity.CRF_TemplateDependencies)
            {
                if (!dependencies.Any(x => x.CRFTemplateDependencyID == dependency.CRFTemplateDependencyID))
                    Context.Delete(dependency);
            }
        }

        public CRF_TemplateDependency AddDependency(CRF_TemplateGroup entity, CRF_TemplateDependency dependency)
        {
            if (dependency.CRFTemplateDependencyID <= 0)
            {
                Context.Add(dependency);
                dependency.CRFTemplateGroupID = entity.CRFTemplateGroupID;
                dependency.CRFTemplateGroup = entity;
            }
            else
            {
                Context.AttachCopy(dependency);
            }

            return dependency;
        }

        public CRF_TemplateDependencySource AddDependencySource(CRF_TemplateDependency entity, CRF_TemplateDependencySource source)
        {
            if (source.CRFTemplateDependencySourceID <= 0)
            {
                Context.Add(source);
                source.CRFTemplateDependencyID = entity.CRFTemplateDependencyID;
                source.CRFTemplateDependency = entity;
            }
            else
            {
                Context.AttachCopy(source);
            }

            return source;
        }

        #endregion
    }
}