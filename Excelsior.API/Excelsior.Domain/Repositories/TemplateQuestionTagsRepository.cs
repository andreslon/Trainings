using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class TemplateQuestionTagsRepository : EntityBaseRepository<CRF_TemplateQuestionTag>, ITemplateQuestionTagsRepository
    {
        #region Constructor

        public TemplateQuestionTagsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions
      
        public IQueryable<CRF_TemplateQuestionTag> GetAll(string search )
        {
            var query = GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.QuestionTagName.Contains(search));
            }
            return query;
        }
         
        #endregion
    }
}