using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeManager.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeManager : IRequest<EmployeeManagerCommandVM>
            {
                public Guid EmployeeId { get; set; }
                public Guid? ManagerId { get; set; }
                public Boolean IsPrimaryManager { get; set; }
                public Guid? AssignedBy { get; set; }
                public DateTime? AssignDate { get; set; }
                public Guid? UnAssignedBy { get; set; }
                public DateTime? UnAssignDate { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
                public Guid? ManagerTypeId { get; set; }
            }

            public class UpdateEmployeeManager : IRequest<EmployeeManagerCommandVM>
            {
                public Guid Id { get; set; }
                public Guid EmployeeId { get; set; }
                public Guid? ManagerId { get; set; }
                public Boolean IsPrimaryManager { get; set; }
                public Guid? AssignedBy { get; set; }
                public DateTime? AssignDate { get; set; }
                public Guid? UnAssignedBy { get; set; }
                public DateTime? UnAssignDate { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
                public Guid? ManagerTypeId { get; set; }
            }

            public class MarkAsDeleteEmployeeManager : IRequest<EmployeeManagerCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
