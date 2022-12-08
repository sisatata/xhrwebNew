using EmployeeEnrollment.Application.Chart.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.Chart.Queries
{
  public static  class Queries
    {
        public class EmployeeConfiramtionChart : IRequest<List<EmployeeConfirmationChartVM>>
        {
            public Guid CompanyId { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }
    }
}
