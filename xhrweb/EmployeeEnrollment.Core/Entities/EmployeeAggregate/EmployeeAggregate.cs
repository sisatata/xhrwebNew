using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using EmployeeEnrollment.Core.Entities.ExtensionMethods;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Core.Entities.EmployeeAggregate
{
    public class EmployeeAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        public EmployeeAggregate(IReadOnlyList<Employee> employees)
        {
            _employees = employees;
            _employee = new Employee(Guid.NewGuid());
        }
        public EmployeeAggregate(Employee employee)
        {
            _employee = employee;
        }
        private readonly IReadOnlyList<Employee> _employees = new List<Employee>();
        public Employee _employee { get; set; }

        public void NewEmployeeCreate(string employeeId,
         Guid companyId,
         Guid? branchId,
         Guid? departmentId,
         Guid positionId,
         string fullName,
         string fulNameLC,
         DateTime? dateOfBirth,
         string referenceNumber,
         Guid nationalityId,
         Guid genderId)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "CompanyId");
            Guard.Against.NullOrEmptyGuid(positionId, "PositionId");
            Guard.Against.NullOrEmptyGuid(nationalityId, "NationalityId");
            Guard.Against.NullOrEmptyGuid(genderId, "GenderId");
            Guard.Against.NullOrWhiteSpace(fullName, "FullName");
            Guard.Against.OutOfSQLDateRange(dateOfBirth.Value, "DateOfBirth");
            Guard.Against.DuplicateEmployeeId( employeeId, _employees, "Employee Id");

            _employee.Create(
                         employeeId,
                         companyId,
                         branchId,
                         departmentId,
                         positionId,
                         fullName,
                         fulNameLC,
                         dateOfBirth,
                         referenceNumber,
                         nationalityId,
                         genderId
                );
        }

        public void StartEmployeeUpdate(string employeeId,
         Guid companyId,
         Guid? branchId,
         Guid? departmentId,
         Guid positionId,
         string fullName,
         string fulNameLC,
         DateTime? dateOfBirth,
         string referenceNumber,
         Guid nationalityId,
         Guid genderId)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "CompanyId");
            Guard.Against.NullOrEmptyGuid(positionId, "PositionId");
            Guard.Against.NullOrEmptyGuid(nationalityId, "NationalityId");
            Guard.Against.NullOrEmptyGuid(genderId, "GenderId");
            Guard.Against.NullOrWhiteSpace(fullName, "FullName");
            Guard.Against.OutOfSQLDateRange(dateOfBirth.Value, "DateOfBirth");
            Guard.Against.DuplicateEmployeeIdUpdateMode(_employee.Id, employeeId, _employees, "Employee Id");

            _employee.UpdateEmployee(
                         employeeId,
                         companyId,
                         branchId,
                         departmentId,
                         positionId,
                         fullName,
                         fulNameLC,
                         dateOfBirth,
                         referenceNumber,
                         nationalityId,
                         genderId
                );
        }

    }
}
