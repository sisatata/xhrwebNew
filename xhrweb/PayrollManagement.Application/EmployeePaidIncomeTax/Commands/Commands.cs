using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeePaidIncomeTax.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeePaidIncomeTax : IRequest<EmployeePaidIncomeTaxCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? EmployeeId { get; set; }
                public string FinancialYear { get; set; }
                public string MonthName { get; set; }
                public Guid? FinancialYearId { get; set; }
                public Guid? MonthCycleId { get; set; }
                public decimal? Basic { get; set; }
                public decimal? HouseRent { get; set; }
                public decimal? Medical { get; set; }
                public decimal? Conveyance { get; set; }
                public decimal? FestivalBonus { get; set; }
                public decimal? TaxAmount { get; set; }
                public DateTime ProcessingDate { get; set; }
                public string Remarks { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class UpdateEmployeePaidIncomeTax : IRequest<EmployeePaidIncomeTaxCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? EmployeeId { get; set; }
                public string FinancialYear { get; set; }
                public string MonthName { get; set; }
                public Guid? FinancialYearId { get; set; }
                public Guid? MonthCycleId { get; set; }
                public decimal? Basic { get; set; }
                public decimal? HouseRent { get; set; }
                public decimal? Medical { get; set; }
                public decimal? Conveyance { get; set; }
                public decimal? FestivalBonus { get; set; }
                public decimal? TaxAmount { get; set; }
                public DateTime ProcessingDate { get; set; }
                public string Remarks { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class MarkAsDeleteEmployeePaidIncomeTax : IRequest<EmployeePaidIncomeTaxCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
