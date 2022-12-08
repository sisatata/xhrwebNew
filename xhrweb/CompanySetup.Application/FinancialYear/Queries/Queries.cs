using System;
using System.Collections.Generic;
using System.Text;
using CompanySetup.Application.FinancialYear.Commands.Models;
using CompanySetup.Application.FinancialYear.Queries.Models;
using MediatR;

namespace CompanySetup.Application.FinancialYear.Queries
{
  public static  class Queries
    {
        public class GetFinancialYearByCompanyId :IRequest<List<FinancialYearVM>>
        { 
            public Guid CompanyId { get; set; }
        }

        public class GetFinancialYearById : IRequest<FinancialYearVM>
        {
            public Guid Id { get; set; }
        }
        public class GetFinancialYearByName: IRequest<FinancialYearIdVM>
        {
            public Guid? CompanyId { get; set; }
            public string FinancialYearName { get; set; }
        }
    }
}
