using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Models
{
    public class PagedResultRequestBase
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
