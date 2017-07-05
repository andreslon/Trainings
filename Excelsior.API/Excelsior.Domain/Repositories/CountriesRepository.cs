using Excelsior.Infrastructure.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Excelsior.Domain.Repositories
{
    public class CountriesRepository : EntityBaseRepository<CONTACT_Country>, ICountriesRepository
    {
        #region Constructor

        public CountriesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<CONTACT_Country> GetAll(string search)
        {
            var query = GetAll();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.CountryName.Contains(search));
            }
            return query;
        }

        #endregion
    }
}
