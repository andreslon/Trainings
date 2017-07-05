using System.Collections.Generic;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class TemplateDependenciesRepository : EntityBaseRepository<CRF_TemplateDependency>, ITemplateDependenciesRepository
    {
        #region Constructor

        public TemplateDependenciesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 
        public IQueryable<CRF_TemplateDependency> GetAll(long? questionId, bool? actionEnable)
        { 
            var trialDependencies = GetAll();

            if (questionId.HasValue)
            {
                trialDependencies = trialDependencies.Where(x => x.TargetQuestionID == questionId);
            }

            if (actionEnable.HasValue)
            {
                trialDependencies = trialDependencies.Where(x => x.ActionEnable == actionEnable);
            } 

            return trialDependencies;
        }

        public IQueryable<CRF_TemplateDependencySource> GetSources(long id)
        {
            return Context.CRF_TemplateDependencySources.Where(x => x.CRFTemplateDependencyID == id);
        }

        public CRF_TemplateDependencySource AddSource(CRF_TemplateDependency entity, CRF_TemplateDependencySource source)
        {
            if (source.CRFTemplateDependencySourceID <= 0)
            {
                Context.Add(source);
            }
            else
            {
                Context.AttachCopy(source);
            }
            source.CRFTemplateDependencyID = entity.CRFTemplateDependencyID;
            source.CRFTemplateDependency = entity;

            return source;
        }

        #endregion
    }
}