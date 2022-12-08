using ASL.AccessControl.Domain;
using ASL.AccessControl.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.PermissionProvider
{
    public class StandardPermissionProvider : DefaultPermissionProvider, IPermissionProvider
    {
        ////admin area permissions
        //public static readonly PermissionRecord ViewEmployee = new PermissionRecord { Name = "View Employee", SystemName = "Permissions.ViewEmployee", Category = "Employee Management" };
        //public static readonly PermissionRecord CreateEmployee = new PermissionRecord { Name = "Create Employee", SystemName = "Permissions.CreateEmployee", Category = "Employee Management" };
        //public static readonly PermissionRecord EditEmployee = new PermissionRecord { Name = "Edit Employee", SystemName = "Permissions.EditEmployee", Category = "Employee Management" };
        //public static readonly PermissionRecord DeleteEmployee = new PermissionRecord { Name = "Delete Employee", SystemName = "Permissions.DeleteEmployee", Category = "Employee Management" };

        //public const string ViewEmployeePermission = "Permissions.ViewEmployee";
        //public const string CreateEmployeePermission = "Permissions.CreateEmployee";
        //public const string EditEmployeePermission = "Permissions.EditEmployee";
        //public const string DeleteEmployeePermission = "Permissions.DeleteEmployee";

        //public override IEnumerable<PermissionRecord> GetPermissions()
        //{
        //    return new[]
        //    {
        //        ViewUser,
        //        CreateUser,
        //        EditUser,
        //        DeleteUser,
        //        ViewEmployee,
        //        CreateEmployee,
        //        EditEmployee,
        //        DeleteEmployee
        //    };
        //}

        //ATTENDANCEPROCESSEDDATA
        public static readonly PermissionRecord ViewAttendanceProcessedData = new PermissionRecord { Name = "View AttendanceProcessedData", SystemName = "Permissions.ViewAttendanceProcessedData", Category = "AttendanceProcessedData" };
        public static readonly PermissionRecord CreateAttendanceProcessedData = new PermissionRecord { Name = "Create AttendanceProcessedData", SystemName = "Permissions.CreateAttendanceProcessedData", Category = "AttendanceProcessedData" };
        public static readonly PermissionRecord EditAttendanceProcessedData = new PermissionRecord { Name = "Edit AttendanceProcessedData", SystemName = "Permissions.EditAttendanceProcessedData", Category = "AttendanceProcessedData" };
        public static readonly PermissionRecord DeleteAttendanceProcessedData = new PermissionRecord { Name = "Delete AttendanceProcessedData", SystemName = "Permissions.DeleteAttendanceProcessedData", Category = "AttendanceProcessedData" };

        public const string ViewAttendanceProcessedDataPermission = "Permissions.ViewAttendanceProcessedData";
        public const string CreateAttendanceProcessedDataPermission = "Permissions.CreateAttendanceProcessedData";
        public const string EditAttendanceProcessedDataPermission = "Permissions.EditAttendanceProcessedData";
        public const string DeleteAttendanceProcessedDataPermission = "Permissions.DeleteAttendanceProcessedData";

        //ATTENDANCEREQUEST
        public static readonly PermissionRecord ViewAttendanceRequest = new PermissionRecord { Name = "View AttendanceRequest", SystemName = "Permissions.ViewAttendanceRequest", Category = "AttendanceRequest" };
        public static readonly PermissionRecord CreateAttendanceRequest = new PermissionRecord { Name = "Create AttendanceRequest", SystemName = "Permissions.CreateAttendanceRequest", Category = "AttendanceRequest" };
        public static readonly PermissionRecord EditAttendanceRequest = new PermissionRecord { Name = "Edit AttendanceRequest", SystemName = "Permissions.EditAttendanceRequest", Category = "AttendanceRequest" };
        public static readonly PermissionRecord DeleteAttendanceRequest = new PermissionRecord { Name = "Delete AttendanceRequest", SystemName = "Permissions.DeleteAttendanceRequest", Category = "AttendanceRequest" };

        public const string ViewAttendanceRequestPermission = "Permissions.ViewAttendanceRequest";
        public const string CreateAttendanceRequestPermission = "Permissions.CreateAttendanceRequest";
        public const string EditAttendanceRequestPermission = "Permissions.EditAttendanceRequest";
        public const string DeleteAttendanceRequestPermission = "Permissions.DeleteAttendanceRequest";

        //EMPLOYEE
        public static readonly PermissionRecord ViewEmployee = new PermissionRecord { Name = "View Employee", SystemName = "Permissions.ViewEmployee", Category = "Employee" };
        public static readonly PermissionRecord CreateEmployee = new PermissionRecord { Name = "Create Employee", SystemName = "Permissions.CreateEmployee", Category = "Employee" };
        public static readonly PermissionRecord EditEmployee = new PermissionRecord { Name = "Edit Employee", SystemName = "Permissions.EditEmployee", Category = "Employee" };
        public static readonly PermissionRecord DeleteEmployee = new PermissionRecord { Name = "Delete Employee", SystemName = "Permissions.DeleteEmployee", Category = "Employee" };

        public const string ViewEmployeePermission = "Permissions.ViewEmployee";
        public const string CreateEmployeePermission = "Permissions.CreateEmployee";
        public const string EditEmployeePermission = "Permissions.EditEmployee";
        public const string DeleteEmployeePermission = "Permissions.DeleteEmployee";

        //EMPLOYEECARD
        public static readonly PermissionRecord ViewEmployeeCard = new PermissionRecord { Name = "View EmployeeCard", SystemName = "Permissions.ViewEmployeeCard", Category = "EmployeeCard" };
        public static readonly PermissionRecord CreateEmployeeCard = new PermissionRecord { Name = "Create EmployeeCard", SystemName = "Permissions.CreateEmployeeCard", Category = "EmployeeCard" };
        public static readonly PermissionRecord EditEmployeeCard = new PermissionRecord { Name = "Edit EmployeeCard", SystemName = "Permissions.EditEmployeeCard", Category = "EmployeeCard" };
        public static readonly PermissionRecord DeleteEmployeeCard = new PermissionRecord { Name = "Delete EmployeeCard", SystemName = "Permissions.DeleteEmployeeCard", Category = "EmployeeCard" };

        public const string ViewEmployeeCardPermission = "Permissions.ViewEmployeeCard";
        public const string CreateEmployeeCardPermission = "Permissions.CreateEmployeeCard";
        public const string EditEmployeeCardPermission = "Permissions.EditEmployeeCard";
        public const string DeleteEmployeeCardPermission = "Permissions.DeleteEmployeeCard";

        //EMPLOYEEDETAIL
        public static readonly PermissionRecord ViewEmployeeDetail = new PermissionRecord { Name = "View EmployeeDetail", SystemName = "Permissions.ViewEmployeeDetail", Category = "EmployeeDetail" };
        public static readonly PermissionRecord CreateEmployeeDetail = new PermissionRecord { Name = "Create EmployeeDetail", SystemName = "Permissions.CreateEmployeeDetail", Category = "EmployeeDetail" };
        public static readonly PermissionRecord EditEmployeeDetail = new PermissionRecord { Name = "Edit EmployeeDetail", SystemName = "Permissions.EditEmployeeDetail", Category = "EmployeeDetail" };
        public static readonly PermissionRecord DeleteEmployeeDetail = new PermissionRecord { Name = "Delete EmployeeDetail", SystemName = "Permissions.DeleteEmployeeDetail", Category = "EmployeeDetail" };

        public const string ViewEmployeeDetailPermission = "Permissions.ViewEmployeeDetail";
        public const string CreateEmployeeDetailPermission = "Permissions.CreateEmployeeDetail";
        public const string EditEmployeeDetailPermission = "Permissions.EditEmployeeDetail";
        public const string DeleteEmployeeDetailPermission = "Permissions.DeleteEmployeeDetail";

        //EMPLOYEEFAMILYMEMBER
        public static readonly PermissionRecord ViewEmployeeFamilyMember = new PermissionRecord { Name = "View EmployeeFamilyMember", SystemName = "Permissions.ViewEmployeeFamilyMember", Category = "EmployeeFamilyMember" };
        public static readonly PermissionRecord CreateEmployeeFamilyMember = new PermissionRecord { Name = "Create EmployeeFamilyMember", SystemName = "Permissions.CreateEmployeeFamilyMember", Category = "EmployeeFamilyMember" };
        public static readonly PermissionRecord EditEmployeeFamilyMember = new PermissionRecord { Name = "Edit EmployeeFamilyMember", SystemName = "Permissions.EditEmployeeFamilyMember", Category = "EmployeeFamilyMember" };
        public static readonly PermissionRecord DeleteEmployeeFamilyMember = new PermissionRecord { Name = "Delete EmployeeFamilyMember", SystemName = "Permissions.DeleteEmployeeFamilyMember", Category = "EmployeeFamilyMember" };

        public const string ViewEmployeeFamilyMemberPermission = "Permissions.ViewEmployeeFamilyMember";
        public const string CreateEmployeeFamilyMemberPermission = "Permissions.CreateEmployeeFamilyMember";
        public const string EditEmployeeFamilyMemberPermission = "Permissions.EditEmployeeFamilyMember";
        public const string DeleteEmployeeFamilyMemberPermission = "Permissions.DeleteEmployeeFamilyMember";

        //EMPLOYEEIMAGE
        public static readonly PermissionRecord ViewEmployeeImage = new PermissionRecord { Name = "View EmployeeImage", SystemName = "Permissions.ViewEmployeeImage", Category = "EmployeeImage" };
        public static readonly PermissionRecord CreateEmployeeImage = new PermissionRecord { Name = "Create EmployeeImage", SystemName = "Permissions.CreateEmployeeImage", Category = "EmployeeImage" };
        public static readonly PermissionRecord EditEmployeeImage = new PermissionRecord { Name = "Edit EmployeeImage", SystemName = "Permissions.EditEmployeeImage", Category = "EmployeeImage" };
        public static readonly PermissionRecord DeleteEmployeeImage = new PermissionRecord { Name = "Delete EmployeeImage", SystemName = "Permissions.DeleteEmployeeImage", Category = "EmployeeImage" };

        public const string ViewEmployeeImagePermission = "Permissions.ViewEmployeeImage";
        public const string CreateEmployeeImagePermission = "Permissions.CreateEmployeeImage";
        public const string EditEmployeeImagePermission = "Permissions.EditEmployeeImage";
        public const string DeleteEmployeeImagePermission = "Permissions.DeleteEmployeeImage";

        //EMPLOYEEMANAGER
        public static readonly PermissionRecord ViewEmployeeManager = new PermissionRecord { Name = "View EmployeeManager", SystemName = "Permissions.ViewEmployeeManager", Category = "EmployeeManager" };
        public static readonly PermissionRecord CreateEmployeeManager = new PermissionRecord { Name = "Create EmployeeManager", SystemName = "Permissions.CreateEmployeeManager", Category = "EmployeeManager" };
        public static readonly PermissionRecord EditEmployeeManager = new PermissionRecord { Name = "Edit EmployeeManager", SystemName = "Permissions.EditEmployeeManager", Category = "EmployeeManager" };
        public static readonly PermissionRecord DeleteEmployeeManager = new PermissionRecord { Name = "Delete EmployeeManager", SystemName = "Permissions.DeleteEmployeeManager", Category = "EmployeeManager" };

        public const string ViewEmployeeManagerPermission = "Permissions.ViewEmployeeManager";
        public const string CreateEmployeeManagerPermission = "Permissions.CreateEmployeeManager";
        public const string EditEmployeeManagerPermission = "Permissions.EditEmployeeManager";
        public const string DeleteEmployeeManagerPermission = "Permissions.DeleteEmployeeManager";

        //EMPLOYEESTATUS
        public static readonly PermissionRecord ViewEmployeeStatus = new PermissionRecord { Name = "View EmployeeStatus", SystemName = "Permissions.ViewEmployeeStatus", Category = "EmployeeStatus" };
        public static readonly PermissionRecord CreateEmployeeStatus = new PermissionRecord { Name = "Create EmployeeStatus", SystemName = "Permissions.CreateEmployeeStatus", Category = "EmployeeStatus" };
        public static readonly PermissionRecord EditEmployeeStatus = new PermissionRecord { Name = "Edit EmployeeStatus", SystemName = "Permissions.EditEmployeeStatus", Category = "EmployeeStatus" };
        public static readonly PermissionRecord DeleteEmployeeStatus = new PermissionRecord { Name = "Delete EmployeeStatus", SystemName = "Permissions.DeleteEmployeeStatus", Category = "EmployeeStatus" };

        public const string ViewEmployeeStatusPermission = "Permissions.ViewEmployeeStatus";
        public const string CreateEmployeeStatusPermission = "Permissions.CreateEmployeeStatus";
        public const string EditEmployeeStatusPermission = "Permissions.EditEmployeeStatus";
        public const string DeleteEmployeeStatusPermission = "Permissions.DeleteEmployeeStatus";

        //GRADE
        public static readonly PermissionRecord ViewGrade = new PermissionRecord { Name = "View Grade", SystemName = "Permissions.ViewGrade", Category = "Grade" };
        public static readonly PermissionRecord CreateGrade = new PermissionRecord { Name = "Create Grade", SystemName = "Permissions.CreateGrade", Category = "Grade" };
        public static readonly PermissionRecord EditGrade = new PermissionRecord { Name = "Edit Grade", SystemName = "Permissions.EditGrade", Category = "Grade" };
        public static readonly PermissionRecord DeleteGrade = new PermissionRecord { Name = "Delete Grade", SystemName = "Permissions.DeleteGrade", Category = "Grade" };

        public const string ViewGradePermission = "Permissions.ViewGrade";
        public const string CreateGradePermission = "Permissions.CreateGrade";
        public const string EditGradePermission = "Permissions.EditGrade";
        public const string DeleteGradePermission = "Permissions.DeleteGrade";

        //OFFICENOTICE
        public static readonly PermissionRecord ViewOfficeNotice = new PermissionRecord { Name = "View OfficeNotice", SystemName = "Permissions.ViewOfficeNotice", Category = "OfficeNotice" };
        public static readonly PermissionRecord CreateOfficeNotice = new PermissionRecord { Name = "Create OfficeNotice", SystemName = "Permissions.CreateOfficeNotice", Category = "OfficeNotice" };
        public static readonly PermissionRecord EditOfficeNotice = new PermissionRecord { Name = "Edit OfficeNotice", SystemName = "Permissions.EditOfficeNotice", Category = "OfficeNotice" };
        public static readonly PermissionRecord DeleteOfficeNotice = new PermissionRecord { Name = "Delete OfficeNotice", SystemName = "Permissions.DeleteOfficeNotice", Category = "OfficeNotice" };

        public const string ViewOfficeNoticePermission = "Permissions.ViewOfficeNotice";
        public const string CreateOfficeNoticePermission = "Permissions.CreateOfficeNotice";
        public const string EditOfficeNoticePermission = "Permissions.EditOfficeNotice";
        public const string DeleteOfficeNoticePermission = "Permissions.DeleteOfficeNotice";

        //SALARYRULE
        public static readonly PermissionRecord ViewSalaryRule = new PermissionRecord { Name = "View SalaryRule", SystemName = "Permissions.ViewSalaryRule", Category = "SalaryRule" };
        public static readonly PermissionRecord CreateSalaryRule = new PermissionRecord { Name = "Create SalaryRule", SystemName = "Permissions.CreateSalaryRule", Category = "SalaryRule" };
        public static readonly PermissionRecord EditSalaryRule = new PermissionRecord { Name = "Edit SalaryRule", SystemName = "Permissions.EditSalaryRule", Category = "SalaryRule" };
        public static readonly PermissionRecord DeleteSalaryRule = new PermissionRecord { Name = "Delete SalaryRule", SystemName = "Permissions.DeleteSalaryRule", Category = "SalaryRule" };

        public const string ViewSalaryRulePermission = "Permissions.ViewSalaryRule";
        public const string CreateSalaryRulePermission = "Permissions.CreateSalaryRule";
        public const string EditSalaryRulePermission = "Permissions.EditSalaryRule";
        public const string DeleteSalaryRulePermission = "Permissions.DeleteSalaryRule";


        public override IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[]
            {
                //ATTENDANCEPROCESSEDDATA
                ViewAttendanceProcessedData,
                CreateAttendanceProcessedData,
                EditAttendanceProcessedData,
                DeleteAttendanceProcessedData, 

                //ATTENDANCEREQUEST
                ViewAttendanceRequest,
                CreateAttendanceRequest,
                EditAttendanceRequest,
                DeleteAttendanceRequest, 

                //EMPLOYEE
                ViewEmployee,
                CreateEmployee,
                EditEmployee,
                DeleteEmployee, 

                //EMPLOYEECARD
                ViewEmployeeCard,
                CreateEmployeeCard,
                EditEmployeeCard,
                DeleteEmployeeCard, 

                //EMPLOYEEDETAIL
                ViewEmployeeDetail,
                CreateEmployeeDetail,
                EditEmployeeDetail,
                DeleteEmployeeDetail, 

                //EMPLOYEEFAMILYMEMBER
                ViewEmployeeFamilyMember,
                CreateEmployeeFamilyMember,
                EditEmployeeFamilyMember,
                DeleteEmployeeFamilyMember,
                
                //EMPLOYEEIMAGE
                ViewEmployeeImage,
                CreateEmployeeImage,
                EditEmployeeImage,
                DeleteEmployeeImage, 

                //EMPLOYEEMANAGER
                ViewEmployeeManager,
                CreateEmployeeManager,
                EditEmployeeManager,
                DeleteEmployeeManager, 

                //EMPLOYEESTATUS
                ViewEmployeeStatus,
                CreateEmployeeStatus,
                EditEmployeeStatus,
                DeleteEmployeeStatus, 

                //GRADE
                ViewGrade,
                CreateGrade,
                EditGrade,
                DeleteGrade, 

                //OFFICENOTICE
                ViewOfficeNotice,
                CreateOfficeNotice,
                EditOfficeNotice,
                DeleteOfficeNotice, 

                //SALARYRULE
                ViewSalaryRule,
                CreateSalaryRule,
                EditSalaryRule,
                DeleteSalaryRule
                 };
        }


    }
}
