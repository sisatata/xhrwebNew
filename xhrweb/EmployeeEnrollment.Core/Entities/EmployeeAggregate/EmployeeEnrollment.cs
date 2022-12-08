using EmployeeEnrollment.Core.Interfaces;
using System;
using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using ASL.Hrms.SharedKernel.Interfaces;

namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeEnrollment : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeId { get; private set; }
        public DateTime JoiningDate { get; private set; }
        public DateTime? QuitDate { get; private set; }
        public Int16 StatusId { get; private set; }
        public DateTime? PermanentDate { get; private set; }
        public Guid? JobTypeId { get; private set; }
        public Guid? GradeId { get; private set; }
        public Guid? SubGradeId { get; private set; }
        public Guid? EmployeeTypeId { get; private set; }
        public Guid? EmployeeCategoryId { get; private set; }
        public Guid? ConfirmationId { get; private set; }
        public Guid? GenderId { get; private set; }
        public bool IsDeleted { get; private set; }
        public string EmployeeImageUri { get; private set; }

        public string LeaveTypeGroup { get; private set; }

        //public int LeaveTypeGroupId { get; private set; }
        public string EmployeeSignature { get; private set; }
        public EmployeeEnrollment(Guid id) : base(id) { }
        private EmployeeEnrollment() : base(Guid.NewGuid()) { }

        public static EmployeeEnrollment Create(

         Guid? employeeId,
         DateTime joiningDate,
         DateTime? quitDate,
         Int16 statusId,
         DateTime? permanentDate,
         Guid? jobTypeId,
         Guid? gradeId,
         Guid? subGradeId,
         Guid? employeeTypeId,
         Guid? employeeCategoryId,
         Guid? confirmationId,
         Guid? genderId
         , string leaveTypeGroup

        )

        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            Guard.Against.OutOfSQLDateRange(joiningDate, "JoiningDate");

            var oModel = new EmployeeEnrollment(Guid.NewGuid());

            oModel.EmployeeId = employeeId;
            oModel.JoiningDate = joiningDate;
            oModel.QuitDate = quitDate;
            oModel.StatusId = statusId;
            oModel.PermanentDate = permanentDate;
            oModel.JobTypeId = jobTypeId;
            oModel.GradeId = gradeId;
            oModel.SubGradeId = subGradeId;
            oModel.EmployeeTypeId = employeeTypeId;
            oModel.EmployeeCategoryId = employeeCategoryId;
            oModel.ConfirmationId = confirmationId;
            oModel.GenderId = genderId;
            oModel.LeaveTypeGroup = leaveTypeGroup;

            return oModel;

        }


        public void UpdateEmployeeEnrollment
            (

         Guid? employeeId,
         DateTime joiningDate,
         DateTime? quitDate,
         Int16 statusId,
         DateTime? permanentDate,
         Guid? jobTypeId,
         Guid? gradeId,
         Guid? subGradeId,
         Guid? employeeTypeId,
         Guid? employeeCategoryId,
         Guid? confirmationId,
         Guid? genderId
            , string leaveTypeGroup

        )
        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            Guard.Against.OutOfSQLDateRange(joiningDate, "JoiningDate");

            EmployeeId = employeeId;
            JoiningDate = joiningDate;
            QuitDate = quitDate;
            StatusId = statusId;
            PermanentDate = permanentDate;
            JobTypeId = jobTypeId;
            GradeId = gradeId;
            SubGradeId = subGradeId;
            EmployeeTypeId = employeeTypeId;
            EmployeeCategoryId = employeeCategoryId;
            ConfirmationId = confirmationId;
            GenderId = genderId;
            LeaveTypeGroup = leaveTypeGroup;
        }

        public void UpdateEmployeeImage(Guid? employeeId, string employeeImageUri)
        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            Guard.Against.NullOrWhiteSpace(employeeImageUri, "EmployeeImageUri");

            EmployeeId = employeeId;
            EmployeeImageUri = employeeImageUri;
        }

        public void UpdateEmployeeSignature(Guid? employeeId, string employeeSignatureUri)
        {
            Guard.Against.NullOrEmptyGuid(employeeId.Value, "EmployeeId");
            Guard.Against.NullOrWhiteSpace(employeeSignatureUri, "EmployeeSignatureUri");

            EmployeeId = employeeId;
            EmployeeSignature = employeeSignatureUri;

        }

        public void MarkAsDeleteEmployeeEnrollment()
        {
            IsDeleted = true;
        }
        public void UpsertStatusId(Int16 statusId)
        {
            StatusId = statusId;
        }

    }
}

