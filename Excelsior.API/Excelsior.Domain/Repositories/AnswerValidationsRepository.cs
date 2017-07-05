using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class AnswerValidationsRepository : EntityBaseRepository<CRF_AnswerValidation>, IAnswerValidationsRepository
    {
        #region Constructor

        public AnswerValidationsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<CRF_AnswerValidation> GetAll(string search)
        {
            var query = GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search)
                    || x.Description.Contains(search)
                    || x.ToolTip.Contains(search)
                    || x.Unit.Contains(search)
                    || (x.PACSTrial == null ? false : x.PACSTrial.TrialName.Contains(search)
                        || x.PACSTrial.TrialAlias.Contains(search)
                        || x.PACSTrial.PrimaryDrugs.Contains(search)
                        || (x.PACSTrial.CFGAnimalSpecy == null ? false : x.PACSTrial.CFGAnimalSpecy.AnimalSpeciesName.Contains(search)
                            || x.PACSTrial.CFGAnimalSpecy.AnimalSpeciesDisplayName.Contains(search))));
            }
            return query;
        }

        public override void Delete(CRF_AnswerValidation entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<CRF_AnswerValidation, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        #endregion
    }
}