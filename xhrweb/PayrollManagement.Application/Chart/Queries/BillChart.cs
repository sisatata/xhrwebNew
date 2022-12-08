using MediatR;
using PayrollManagement.Application.Chart.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.Chart.Queries
{
   public class BillChart : IRequest<ChartBillVM>
    {
        public Guid? CompanyId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
