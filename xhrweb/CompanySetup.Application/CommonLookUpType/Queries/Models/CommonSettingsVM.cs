using ASL.Hrms.SharedKernel.Enums;
using CompanySetup.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Core.Entities;

namespace CompanySetup.Application.CommonLookUpType.Queries.Models
{
    public class CommonSettingsVM
    {
        public List<LeaveType> LeaveTypes { get; set; }
        public List<LeaveType> AdminLeaveTypes { get; set; }
        public List<CommonLookup> PunchTypes { get; set; }
        public List<CommonLookup> AttendanceRequestTypes { get; set; }
        public List<BillType> BillTypes { get; set; }
        public List<TaskCategory> TaskCategories { get; set; }
        public List<CommonLookup> ApprovalStatusList { get; set; }
        public List<CommonLookup> HalfDayLeaveTypeList { get; set; }
        public List<CommonLookup> TaskStatuses { get; set; }

        public List<CommonLookup> ClockTimeTypeList { get; set; }
        public List<EmployeeWiseCustomConfiguration> EmployeeWiseCustomConfigurationList { get; set; }

        public List<CompanySetup.Core.Entities.Department> DepartmentList { get; set; }

        public List<CommonLookUpTypeVM> VehicleTypeList { get; set; }
    }

    public class CommonLookup
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string  Value { get; set; }

    }
}
