using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace Excelsior.Domain.Repositories
{
    public class StencilsRepository : EntityBaseRepository<MEA_Stencil>, IStencilsRepository
    {
        #region Constructor

        public StencilsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<MEA_Stencil> GetAll(long? trialId)
        {
            var query = GetAll();

            var andFilters = new List<string>();
            var parameters = new Dictionary<string, object>();

            if (trialId.HasValue)
            {
                var f = @"TrialID == @trialId";
                andFilters.Add(f);
                parameters.Add("@trialId", trialId);
            }

            if (andFilters.Count > 0)
            {
                var filter = CombinePredicates(andFilters, "&&");
                query = query.Where(filter, parameters);
            }

            return query;
        }
        #endregion
    }
}
