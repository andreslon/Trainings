using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excelsior.Business.DtoEntities
{
    public class ResultInfo<T>
    {
        public ResultInfo()
        {
            Exception = string.Empty;
            IsSuccess = false;
            Message = string.Empty;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public T Result { get; set; }
        public Pager Pager { get; set; }
        public void SetPager(int? itemCount, int? pageIndex, int? pageSize)
        {
            Pager = new Pager(itemCount, pageIndex, pageSize);
        }
    }
}
