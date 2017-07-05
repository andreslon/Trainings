using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelsior.Domain.Repositories
{
    public class AnimalSpeciesRepository : EntityBaseRepository<CFG_AnimalSpecy>, IAnimalSpeciesRepository
    {
        #region Constructor

        public AnimalSpeciesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions

        public IQueryable<CFG_AnimalSpecy> GetAll(string search)
        {
            var query = GetAll();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x => x.AnimalSpeciesName.Contains(search)
                    || x.AnimalSpeciesDisplayName.Contains(search));
            }

            return query;
        }

        #endregion
    }
}
