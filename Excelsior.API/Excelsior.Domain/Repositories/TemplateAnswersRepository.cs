using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class TemplateAnswersRepository : EntityBaseRepository<CRF_TemplateAnswer>, ITemplateAnswersRepository
    {
        #region Constructor

        public TemplateAnswersRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<CRF_TemplateAnswer> GetAll(long? questionId, string search)
        {
            var answers = GetAll();

            if (questionId.HasValue)
            {
                answers = answers.Where(x => x.CRFTemplateQuestionID == questionId);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                answers = answers.Where(x => x.AltAnswerString.Contains(search)
                    || x.AnswerString.Contains(search));
            }

            return answers.Select(x => x);
        }

        public override void Delete(CRF_TemplateAnswer entity)
        {
            entity.IsActive = false;
        }

        public override void DeleteWhere(Expression<Func<CRF_TemplateAnswer, bool>> predicate)
        {
            FindBy(predicate).ForEach(x => x.IsActive = false);
        }

        #endregion
    }
}