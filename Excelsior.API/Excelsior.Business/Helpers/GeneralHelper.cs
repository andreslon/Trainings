using Excelsior.Business.DtoEntities;
using Excelsior.Domain.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace Excelsior.Business.Helpers
{
    public static class GeneralHelper
    {
        public static List<T> GetPagedList<T>(IQueryable<T> list, Pager pager)
        {
            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                if (pager.PageSize == 0)
                    return list.ToList();
                return list.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).ToList();
            }); 
            return result;
        }

        public static List<dynamic> GetPagedList(IQueryable list, Pager pager)
        {
            var result = DataHelpers.RetryPolicy.ExecuteAction(() =>
            {
                if (pager.PageSize == 0)
                    return list.Cast<dynamic>().ToList();
                return list.Skip((pager.PageIndex - 1) * pager.PageSize).Take(pager.PageSize).Cast<dynamic>().ToList();
            });
            return result;
        }
    }
}
