using ASL.Utility.FileManager.Interfaces;
using EmployeeEnrollment.Application.Employee.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EmployeeEnrollment.Application.Employee.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployee : IRequest<EmployeeCommandVM>
            {
                public string EmployeeId { get; set; }
                public Guid CompanyId { get; set; }
                public Guid? BranchId { get; set; }
                public Guid? DepartmentId { get; set; }
                public Guid PositionId { get; set; }
                public string FullName { get; set; }
                public string FullNameLC { get; set; }
                public DateTime? DateOfBirth { get; set; }
                public string ReferenceNumber { get; set; }
                public Guid NationalityId { get; set; }
                public Guid GenderId { get; set; }
                public string UserId { get; set; }
                public string Password { get; set; }
            }

            public class UpdateEmployee : IRequest<EmployeeCommandVM>
            {
                public Guid Id { get; set; }
                public string EmployeeId { get; set; }
                public Guid CompanyId { get; set; }
                public Guid? BranchId { get; set; }
                public Guid? DepartmentId { get; set; }
                public Guid PositionId { get; set; }
                public string FullName { get; set; }
                public string FullNameLC { get; set; }
                public DateTime? DateOfBirth { get; set; }
                public string ReferenceNumber { get; set; }
                public Guid NationalityId { get; set; }
                public Guid GenderId { get; set; }
            }

            public class MarkAsDeleteEmployee : IRequest<EmployeeDeleteCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class UpdateLoginIdEmployee : IRequest<EmployeeCommandVM>
            {
                public Guid Id { get; set; }
                public Guid? LoginId { get; set; }
            }

            public class UpdateEmployeeTransferData : IRequest<EmployeeCommandVM>
            {
                public Guid Id { get; set; }
                public Guid CompanyId { get; set; }
                public Guid? BranchId { get; set; }
                public Guid? DepartmentId { get; set; }
                public Guid PositionId { get; set; }
            }
            public class EmployeeListExport: IRequest<List<EmployeeExportVM>>
            {
                public List<Guid> BranchIds { get; set; }
                public List<Guid> DepartmentIds { get; set; }
                public List<Guid> DesignationIds { get; set; }
                public string SearchText { get; set; }
                public bool GetAll { get; set; }
            }
            public class EmployeeExportVM : IExcelDataDynamicType
            {
                [Description("Employee Id")]
                public string EmployeeId { get; set; }
                [Description("Employee Name")]
                public string FullName { get; set; }
                [Description("Branch")]
                public string BranchName { get; set; }
                [Description("Department")]
                public string DepartmentName { get; set; }
                [Description("Designation")]
                public string PositionName { get; set; }
                [Description("Date Of Birth")]
                public DateTime? DateOfBirth { get; set; }
                [Description("Nationality")]
                public string NationalityName { get; set; }
                [Description("Gender")]
                public string GenderName { get; set; }
                [Description("Grade")]
                public string GradeName { get; set; }
                [Description("Email")]
                public string LoginId { get; set; }
                public bool IsBoldRow { get; set; }
                public bool WillRemoveColumn { get; set; }

            }



        }
    }
}



