using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.EmployeeLeaveEncashment.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class StartEmployeeLeaveEncashment : IRequest<EmployeeLeaveEncashmentCommandVM>
            {
                public Guid CompanyId { get; set; }
                public Guid? EmployeeId { get; set; }
                public Guid? LeaveTypeId { get; set; }
                public DateTime EncashDate { get; set; }
                public Guid? FinancialYearId { get; set; }
                public Guid? MonthCycleId { get; set; }
                public decimal? ELEncashedDays { get; set; }
                //public decimal? EncashedAmount { get; set; }
                public string Remarks { get; set; }
            }

            public class UpdateEmployeeLeaveEncashment : IRequest<EmployeeLeaveEncashmentCommandVM>
            {
                public Guid? Id { get; set; }
                public Guid? EmployeeId { get; set; }
                public Guid? LeaveTypeId { get; set; }
                public DateTime EncashDate { get; set; }
                public Guid? FinancialYearId { get; set; }
                public Guid? MonthCycleId { get; set; }
                public decimal? ELEncashedDays { get; set; }
                public decimal? EncashedAmount { get; set; }
                public string Remarks { get; set; }
            }

            public class MarkAsDeleteEmployeeLeaveEncashment : IRequest<EmployeeLeaveEncashmentCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
