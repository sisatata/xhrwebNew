using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.Hrms.SharedKernel.Models
{
    public class PagedResult<T, U> : PagedResultBase
    {
        public IList<T> data { get; set; }
        public IList<U> ViewModel { get; set; }

        public PagedResult()
        {
            data = new List<T>();
            ViewModel = new List<U>();
        }
    }

    public class PagedResult<T> : PagedResultBase
    {
        public IList<T> data { get; set; }

        public PagedResult()
        {
            data = new List<T>();
        }
    }
}
