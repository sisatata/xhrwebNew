using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeSalaryProcessedData.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class GenerateEmployeeSalaryProcessedData : IRequest<EmployeeSalaryProcessedDataCommandVM>
            {
                public Guid? EmployeeId { get; set; }
                public Guid? FinancialYearId { get; set; }
                public Guid? MonthCycleId { get; set; }
                //public Int16? PaymentOptionId { get; set; }
                public Guid? CompanyId { get; set; }
            }

            public class UpdateEmployeeSalaryProcessedData : IRequest<EmployeeSalaryProcessedDataCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? EmployeeId { get; set; }
                public Guid? FinancialYearId { get; set; }
                public Guid? MonthCycleId { get; set; }
                public Int16? PaymentOptionId { get; set; }
                public Guid? CompanyId { get; set; }
                public Guid? BranchId { get; set; }
                public Guid? DepartmentId { get; set; }
                public Guid? PositionId { get; set; }
                public Guid? GradeId { get; set; }
                public Int16? TotalDaysInMonth { get; set; }
                public Int16? TotalWorkingDays { get; set; }
                public Int16? TotalPresentDays { get; set; }
                public Int16? TotalAbsentDays { get; set; }
                public Int16? TotalLeaveDays { get; set; }
                public Int16? WeeklyOffDays { get; set; }
                public Int16? GovernmentOffDays { get; set; }
                public Int16? TotalWorkingHoliday { get; set; }
                public decimal? OTHour { get; set; }
                public decimal? OTRate { get; set; }
                public decimal? GrossSalary { get; set; }
                public decimal? PayableSalary { get; set; }
                public decimal? TotalDeductionAmount { get; set; }
                public decimal? NetPayableAmount { get; set; }
                public DateTime? ProcessDate { get; set; }
                public Int16? StampCost { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? EmloyeeSalaryId { get; set; }
                public Boolean IsApproved { get; set; }
                public Guid? ApprovedBy { get; set; }
                public DateTime? ApprovedTime { get; set; }
                public string Remarks { get; set; }
            }

            public class MarkAsDeleteEmployeeSalaryProcessedData : IRequest<EmployeeSalaryProcessedDataCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
