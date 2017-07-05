using Excelsior.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace Excelsior.Domain.Repositories
{
    public class CertQuestionsRepository : EntityBaseRepository<CERT_QuestionList>, ICertQuestionsRepository
    {
        #region Constructor

        public CertQuestionsRepository(DataModel context) : base(context)
        {
        }

        #endregion

        #region Functions 

        public IQueryable<CERT_QuestionList> GetAll(long? procedureId, string search)
        {
            var query = GetAll();

            var andFilters = new List<string>();
            var parameters = new Dictionary<string, object>();

            if (procedureId.HasValue)
            {
                var fi = @"ImgProcedureID == @procedureId";
                andFilters.Add(fi);
                parameters.Add("@procedureId", procedureId);
            }

            if (!string.IsNullOrEmpty(search))
            {
                var orFilters = new List<string>();
                parameters.Add("@search", search);

                if (!procedureId.HasValue)
                {
                    var fi = @"CERTImgProcedureList.ImgProcedureName.Contains(@search)
                        || CERTImgProcedureList.ImgProcedureDescription.Contains(@search)";
                    orFilters.Add(fi);
                }

                var f1 = @"CertQuestionDes.Contains(@search)";
                orFilters.Add(f1);

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
