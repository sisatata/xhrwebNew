using EmployeeEnrollment.Core.Interfaces;
using System;
using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;

namespace EmployeeEnrollment.Core.Entities
{
    public class Employee : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        //public Int64 Id { get; private set; }
        public string EmployeeId { get; private set; }
        public Guid CompanyId { get; private set; }
        public Guid? BranchId { get; private set; }
        public Guid? DepartmentId { get; private set; }
        public Guid PositionId { get; private set; }
        public string FullName { get; private set; }
        public string FullNameLC { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string ReferenceNumber { get; private set; }
        public Guid NationalityId { get; private set; }
        public Guid GenderId { get; private set; }

        public Guid? LoginId { get; private set; }
        public bool IsDeleted { get; private set; }
        //public Int16 StatusId { get; private set; }
        public Employee(Guid id) : base(id) { }

        private Employee() : base(Guid.NewGuid()) { }

        public void Create(string employeeId,
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

            //var employee = new Employee(Guid.NewGuid());
            EmployeeId = employeeId;
            CompanyId = companyId;
            BranchId = branchId;
            DepartmentId = departmentId;
            PositionId = positionId;
            FullName = fullName;
            FullNameLC = fulNameLC;
            DateOfBirth = dateOfBirth;
            ReferenceNumber = referenceNumber;
            NationalityId = nationalityId;
            GenderId = genderId;
            IsDeleted = false;
            //StatusId = 1;
        }

        public void UpdateEmployee(
         string employeeId,
         Guid companyId,
         Guid? branchId,
         Guid? departmentId,
         Guid positionId,
         string fullName,
         string fulNameLC,
         DateTime? dateOfBirth,
         string referenceNumber,
         Guid nationalityId,
         Guid genderId

        )
        {
            Guard.Against.NullOrEmptyGuid(Id, "Id");

            Guard.Against.NullOrEmptyGuid(companyId, "CompanyId");
            Guard.Against.NullOrEmptyGuid(positionId, "PositionId");
            Guard.Against.NullOrEmptyGuid(nationalityId, "NationalityId");
            Guard.Against.NullOrEmptyGuid(genderId, "GenderId");
            Guard.Against.NullOrWhiteSpace(fullName, "FullName");
            Guard.Against.OutOfSQLDateRange(dateOfBirth.Value, "DateOfBirth");

            EmployeeId = employeeId;
            CompanyId = companyId;
            BranchId = branchId;
            DepartmentId = departmentId;
            PositionId = positionId;
            FullName = fullName;
            FullNameLC = fulNameLC;
            DateOfBirth = dateOfBirth;
            ReferenceNumber = referenceNumber;
            NationalityId = nationalityId;
            GenderId = genderId;
        }
        public void MarkAsDelete()
        {
            Guard.Against.NullOrEmptyGuid(Id, "Id");
            IsDeleted = true;
        }
        public void UpsertLoginId(Guid loginId)
        {
            LoginId = loginId;
        }

        public void UpdateEmployeeTransferData(Guid companyId,
         Guid? branchId,
         Guid? departmentId,
         Guid positionId)
        {
            Guard.Against.NullOrEmptyGuid(companyId, "CompanyId");
            Guard.Against.NullOrEmptyGuid(positionId, "PositionId");
            CompanyId = companyId;
            BranchId = branchId;
            DepartmentId = departmentId;
            PositionId = positionId;
        }


    }
}
