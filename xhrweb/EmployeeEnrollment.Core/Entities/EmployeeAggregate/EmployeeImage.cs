using EmployeeEnrollment.Core.Interfaces;
using System;
using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeImage : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid? EmployeeImageId { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public Guid? FamilyMemberId { get; private set; }
        public string Photo { get; private set; }
        public Guid? PhotoId { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }


        public EmployeeImage(Guid id) : base(id) { }
        private EmployeeImage() : base(Guid.NewGuid()) { }

        public static EmployeeImage Create(

         Guid? employeeImageId,
         Guid? employeeId,
         Guid? familyMemberId,
         string photo,
         Guid? photoId,
         Guid? companyId,
         Boolean isDeleted

        )

        {
            var oModel = new EmployeeImage(Guid.NewGuid());

            oModel.EmployeeImageId = employeeImageId;
            oModel.EmployeeId = employeeId;
            oModel.FamilyMemberId = familyMemberId;
            oModel.Photo = photo;
            oModel.PhotoId = photoId;
            oModel.CompanyId = companyId;
            oModel.IsDeleted = isDeleted;

            return oModel;

        }


        public void UpdateEmployeeImage
            (

         Guid? employeeImageId,
         Guid? employeeId,
         Guid? familyMemberId,
         string photo,
         Guid? photoId,
         Guid? companyId,
         Boolean isDeleted
    
        )
        {
            EmployeeImageId = employeeImageId;
            EmployeeId = employeeId;
            FamilyMemberId = familyMemberId;
            Photo = photo;
            PhotoId = photoId;
            CompanyId = companyId;
            IsDeleted = isDeleted;
        }


        public void MarkAsDeleteEmployeeImage()
        {
            IsDeleted = true;
        }


    }
}

