using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities
{
    public class Pager
    {
        public Pager(int? itemCount, int? pageIndex, int? pageSize)
        {
            ItemCount = itemCount ?? 0;
            PageIndex = pageIndex ?? 1;
            PageSize = pageSize ?? 10;
            if (PageSize == 0)
            {
                PageCount = 1;
            }
            else
            {
                var pageCount = (int)Math.Ceiling((decimal)ItemCount / (decimal)PageSize);
                PageCount = pageCount;
            }
        } 

        public int ItemCount { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int PageCount { get; private set; }
    }
}
