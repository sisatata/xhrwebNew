using EmployeeEnrollment.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEnrollment.Persistence.Configurations
{

    public class EmployeeRawDataPrepConfiguration : IEntityTypeConfiguration<EmployeeRawDataPrep>
    {

        public void Configure(EntityTypeBuilder<EmployeeRawDataPrep> builder)
        {
            builder.Ignore(hr => hr.State);
            builder.Property(hr => hr.EmployeeCode).HasMaxLength(250);
            builder.Property(hr => hr.Gross).HasMaxLength(250);
            builder.Property(hr => hr.FullName).HasMaxLength(250);
            builder.Property(hr => hr.NID).HasMaxLength(250);
            builder.Property(hr => hr.BID).HasMaxLength(250);
            builder.Property(hr => hr.MobileNo).HasMaxLength(250);
            builder.Property(hr => hr.JoiningDate).HasMaxLength(250);
            builder.Property(hr => hr.DOB).HasMaxLength(250);
            builder.Property(hr => hr.Gender).HasMaxLength(250);
            builder.Property(hr => hr.Nationality).HasMaxLength(250);
            builder.Property(hr => hr.NightBill).HasMaxLength(250);
            builder.Property(hr => hr.OT).HasMaxLength(250);
            builder.Property(hr => hr.Religion).HasMaxLength(250);
            builder.Property(hr => hr.Branch).HasMaxLength(250);
            builder.Property(hr => hr.Department).HasMaxLength(250);
            builder.Property(hr => hr.Section).HasMaxLength(250);
            builder.Property(hr => hr.StaffCategory).HasMaxLength(250);
            builder.Property(hr => hr.Company).HasMaxLength(250);
            builder.Property(hr => hr.Country).HasMaxLength(250);
            builder.Property(hr => hr.MaritalStatus).HasMaxLength(250);
            builder.Property(hr => hr.BloodGroup).HasMaxLength(250);
            builder.Property(hr => hr.PresentAddress).HasMaxLength(500);
            builder.Property(hr => hr.PermanentAddress).HasMaxLength(250);
            builder.Property(hr => hr.BankName).HasMaxLength(250);
            builder.Property(hr => hr.BankAccount).HasMaxLength(250);
            builder.Property(hr => hr.FullNameBangla).HasMaxLength(250);
            builder.Property(hr => hr.NameofFather).HasMaxLength(250);
            builder.Property(hr => hr.NameofSpouce).HasMaxLength(250);
            builder.Property(hr => hr.NameofMother).HasMaxLength(250);
            builder.Property(hr => hr.Position).HasMaxLength(250);
            builder.Property(hr => hr.Status).HasMaxLength(1);
            builder.Property(hr => hr.ErrorDescription).HasMaxLength(250);
            builder.Property(hr => hr.FileName).HasMaxLength(250);
            builder.Property(hr => hr.JobType).HasMaxLength(250);
            builder.Property(hr => hr.PermanentDate).HasMaxLength(250);
            builder.Property(hr => hr.Emailaddress).HasMaxLength(250);
            builder.Property(hr => hr.EmergencyContact).HasMaxLength(250);
            builder.Property(hr => hr.PermanentDistrict).HasMaxLength(250);
            builder.Property(hr => hr.PermanentThana).HasMaxLength(250);
            builder.Property(hr => hr.PermanentPostOffice).HasMaxLength(250);
            builder.Property(hr => hr.PermanentVillage).HasMaxLength(250);
            builder.Property(hr => hr.PermanentAddressLine1).HasMaxLength(250);
            builder.Property(hr => hr.PermanentAddressLine2).HasMaxLength(250);
            builder.Property(hr => hr.PresentDistrict).HasMaxLength(250);
            builder.Property(hr => hr.PresentThana).HasMaxLength(250);
            builder.Property(hr => hr.PresentPostOffice).HasMaxLength(250);
            builder.Property(hr => hr.PresentVillage).HasMaxLength(250);
            builder.Property(hr => hr.PresentAddressLine1).HasMaxLength(250);
            builder.Property(hr => hr.PresentAddressLine2).HasMaxLength(250);

            builder.Property(hr => hr.PresentCountry).HasMaxLength(250);
            builder.Property(hr => hr.PermanentCountry).HasMaxLength(250);
            builder.Property(hr => hr.Grade).HasMaxLength(250);
            builder.Property(hr => hr.LeaveTypeGroupName).HasMaxLength(250);
            builder.Property(hr => hr.ConfirmationRuleName).HasMaxLength(250);
            builder.Property(hr => hr.SalaryStructureName).HasMaxLength(250);
            builder.Property(hr => hr.PaymentOptionName).HasMaxLength(250);

        }
    }
}

