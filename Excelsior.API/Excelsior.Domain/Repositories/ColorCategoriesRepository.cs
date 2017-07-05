using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;

namespace Excelsior.Domain.Repositories
{
    public class ColorCategoriesRepository : EntityBaseRepository<WF_CategoryFlag>, IColorCategoriesRepository
    {
        #region Constructor

        public ColorCategoriesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<WF_CategoryFlag> GetAll(string search)
        {
            var query = GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.CategoryDes.Contains(search));
            }
            return query;
        }

        #endregion
    }
}
