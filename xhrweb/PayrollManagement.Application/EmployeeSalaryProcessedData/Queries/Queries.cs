using PayrollManagement.Application.EmployeeSalaryProcessedData.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace PayrollManagement.Application.EmployeeSalaryProcessedData.Queries
{
    public static class Queries
    {
        public class GetEmployeeSalaryProcessedDataListByCompany : IRequest<List<EmployeeSalaryProcessedDataVM>>
        {
            public Guid CompanyId { get; set; }
            public Guid FinancialYearId { get; set; }
            public Guid MonthCycleId { get; set; }
        }

        public class GetEmployeeSalaryProcessedDataListByEmployee : IRequest<List<EmployeeSalaryProcessedDataVM>>
        {
            public Guid EmployeeId { get; set; }
            public Guid FinancialYearId { get; set; }
            public Guid MonthCycleId { get; set; }
        }

        public class GetEmployeeSalaryProcessedData : IRequest<EmployeeSalaryProcessedDataVM>
        {
            public Guid Id { get; set; }
        }
       public class GetEmployeeSalaryProcessedDataWithFilter : IRequest<List<EmployeeSalaryProcessedDataVM>>
        {
            public Guid EmployeeId { get; set; }
            public Guid FinancialYearId { get; set; }
            public Guid MonthCycleId { get; set; }
            public Guid CompanyId { get; set; }
            public List<Guid> BranchIds { get; set; }
            public List<Guid> DepartmentIds { get; set; }
            public List<Guid> DesignationIds { get; set; }
            public string SearchText { get; set; }
        }
    }
}
