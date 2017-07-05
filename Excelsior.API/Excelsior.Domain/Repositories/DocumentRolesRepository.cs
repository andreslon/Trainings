using Excelsior.Domain.Helpers;
using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace Excelsior.Domain.Repositories
{
    public class DocucmentRolesRepository : EntityBaseRepository<DOCU_DocumentRole>, IDocumentRolesRepository
    {
        #region Constructor

        public DocucmentRolesRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<DOCU_DocumentRole> GetAll(long? documentId, string search)
        {
            var query = GetAll();
            var andFilters = new List<string>();
            var parameters = new Dictionary<string, object>();

            if (documentId.HasValue)
            {
                var fi = @"DocumentID == @documentId";
                andFilters.Add(fi);
                parameters.Add("@documentId", documentId);
            }

            if (andFilters.Count > 0)
            {
                var filter = CombinePredicates(andFilters, "&&");
                query = query.Where(filter, parameters);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                var orFilters = new List<string>();
                parameters.Add("@search", search);
                var f = @"AspnetRole.RoleName.Contains(@search)";
                orFilters.Add(f);
                var filter = CombinePredicates(orFilters, "||");
                if (andFilters.Count > 0)
                {
                    var andFilter = CombinePredicates(andFilters, "&&");
                    filter = CombinePredicates(new string[] { andFilter, filter }, "&&");
                }
                query = query.Where(filter, parameters);
            }
            else
            {
                if (andFilters.Count > 0)
                {
                    var filter = CombinePredicates(andFilters, "&&");
                    query = query.Where(filter, parameters);
                }
            }
            return query;
        }
        #endregion
    }
}
