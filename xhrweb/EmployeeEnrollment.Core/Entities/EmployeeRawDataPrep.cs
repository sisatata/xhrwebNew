using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeRawDataPrep : BaseEntity<Int64>, IAggregateRoot
    {
        //public Int64? ID { get;  set; }
        public string EmployeeCode { get; set; }
        public string Gross { get; set; }
        public string FullName { get; set; }
        public string NID { get; set; }
        public string BID { get; set; }
        public string MobileNo { get; set; }
        public string JoiningDate { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string NightBill { get; set; }
        public string OT { get; set; }
        public string Religion { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string StaffCategory { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodGroup { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string FullNameBangla { get; set; }
        public string NameofFather { get; set; }
        public string NameofSpouce { get; set; }
        public string NameofMother { get; set; }
        public Guid? PlanetEmployeeID { get; set; }
        public string Position { get; set; }
        public string ErrorDescription { get; set; }
        public string FileName { get; set; }
        public string JobType { get; set; }
        public string PermanentDate { get; set; }
        public string Emailaddress { get; set; }
        public string EmergencyContact { get; set; }
        public string PermanentCountry { get; set; }
        public string PermanentDistrict { get; set; }
        public string PermanentThana { get; set; }
        public string PermanentPostOffice { get; set; }
        public string PermanentVillage { get; set; }
        public string PermanentAddressLine1 { get; set; }
        public string PermanentAddressLine2 { get; set; }
        public string PresentCountry { get; set; }
        public string PresentDistrict { get; set; }
        public string PresentThana { get; set; }
        public string PresentPostOffice { get; set; }
        public string PresentVillage { get; set; }
        public string PresentAddressLine1 { get; set; }
        public string PresentAddressLine2 { get; set; }
        public string Status { get; set; }

        public string Grade { get; set; }

        public string LeaveTypeGroupName { get; set; }
        public string ConfirmationRuleName { get; set; }
        public string SalaryStructureName { get; set; }

        public string PaymentOptionName { get; set; }
        public Guid RawFileDetailId { get; set; }
        // not persisted
        public TrackingState State { get; set; }
        public EmployeeRawDataPrep(Int64 id) : base(id) { }
        EmployeeRawDataPrep() : base(0) { }

        public static EmployeeRawDataPrep Create(

         string employeeCode,
         string gross,
         string fullName,
         string nID,
         string bID,
         string mobileNo,
         string joiningDate,
         string dOB,
         string gender,
         string nationality,
         string nightBill,
         string oT,
         string religion,
         string branch,
         string department,
         string section,
         string staffCategory,
         string company,
         string country,
         string maritalStatus,
         string bloodGroup,
         string presentAddress,
         string permanentAddress,
         string bankName,
         string bankAccount,
         string fullNameBangla,
         string nameofFather,
         string nameofSpouce,
         string nameofMother,
         Guid? planetEmployeeID,
         string position,
         string errorDescription,
         string fileName,
         string jobType,
         string permanentDate,
         string emailaddress,
         string emergencyContact,
         string permanentDistrict,
         string permanentThana,
         string permanentPostOffice,
         string permanentVillage,
         string permanentAddressLine1,
         string permanentAddressLine2,
         string presentDistrict,
         string presentThana,
         string presentPostOffice,
         string presentVillage,
         string presentAddressLine1,
         string presentAddressLine2,
        string permanentCountry,
        string presentCountry,
        string grade

        )

        {
            var oModel = new EmployeeRawDataPrep();

            oModel.EmployeeCode = employeeCode;
            oModel.Gross = gross;
            oModel.FullName = fullName;
            oModel.NID = nID;
            oModel.BID = bID;
            oModel.MobileNo = mobileNo;
            oModel.JoiningDate = joiningDate;
            oModel.DOB = dOB;
            oModel.Gender = gender;
            oModel.Nationality = nationality;
            oModel.NightBill = nightBill;
            oModel.OT = oT;
            oModel.Religion = religion;
            oModel.Branch = branch;
            oModel.Department = department;
            oModel.Section = section;
            oModel.StaffCategory = staffCategory;
            oModel.Company = company;
            oModel.Country = country;
            oModel.MaritalStatus = maritalStatus;
            oModel.BloodGroup = bloodGroup;
            oModel.PresentAddress = presentAddress;
            oModel.PermanentAddress = permanentAddress;
            oModel.BankName = bankName;
            oModel.BankAccount = bankAccount;
            oModel.FullNameBangla = fullNameBangla;
            oModel.NameofFather = nameofFather;
            oModel.NameofSpouce = nameofSpouce;
            oModel.NameofMother = nameofMother;
            oModel.PlanetEmployeeID = planetEmployeeID;
            oModel.Position = position;
            oModel.ErrorDescription = errorDescription;
            oModel.FileName = fileName;
            oModel.JobType = jobType;
            oModel.PermanentDate = permanentDate;
            oModel.Emailaddress = emailaddress;
            oModel.EmergencyContact = emergencyContact;
            oModel.PermanentDistrict = permanentDistrict;
            oModel.PermanentThana = permanentThana;
            oModel.PermanentPostOffice = permanentPostOffice;
            oModel.PermanentVillage = permanentVillage;
            oModel.PermanentAddressLine1 = permanentAddressLine1;
            oModel.PermanentAddressLine2 = permanentAddressLine2;
            oModel.PresentDistrict = presentDistrict;
            oModel.PresentThana = presentThana;
            oModel.PresentPostOffice = presentPostOffice;
            oModel.PresentVillage = presentVillage;
            oModel.PresentAddressLine1 = presentAddressLine1;
            oModel.PresentAddressLine2 = presentAddressLine2;

            oModel.PermanentCountry = permanentCountry;
            oModel.PermanentCountry = presentCountry;
            oModel.Grade = grade;

            return oModel;

        }


        public void UpdateEmployeeRawDataPrep
            (

         string employeeCode,
         string gross,
         string fullName,
         string nID,
         string bID,
         string mobileNo,
         string joiningDate,
         string dOB,
         string gender,
         string nationality,
         string nightBill,
         string oT,
         string religion,
         string branch,
         string department,
         string section,
         string staffCategory,
         string company,
         string country,
         string maritalStatus,
         string bloodGroup,
         string presentAddress,
         string permanentAddress,
         string bankName,
         string bankAccount,
         string fullNameBangla,
         string nameofFather,
         string nameofSpouce,
         string nameofMother,
         Guid? planetEmployeeID,
         string position,
         string errorDescription,
         string fileName,
         string jobType,
         string permanentDate,
         string emailaddress,
         string emergencyContact,
         string permanentDistrict,
         string permanentThana,
         string permanentPostOffice,
         string permanentVillage,
         string permanentAddressLine1,
         string permanentAddressLine2,
         string presentDistrict,
         string presentThana,
         string presentPostOffice,
         string presentVillage,
         string presentAddressLine1,
         string presentAddressLine2,
    string permanentCountry,
    string presentCountry,
    string grade
        )
        {
            EmployeeCode = employeeCode;
            Gross = gross;
            FullName = fullName;
            NID = nID;
            BID = bID;
            MobileNo = mobileNo;
            JoiningDate = joiningDate;
            DOB = dOB;
            Gender = gender;
            Nationality = nationality;
            NightBill = nightBill;
            OT = oT;
            Religion = religion;
            Branch = branch;
            Department = department;
            Section = section;
            StaffCategory = staffCategory;
            Company = company;
            Country = country;
            MaritalStatus = maritalStatus;
            BloodGroup = bloodGroup;
            PresentAddress = presentAddress;
            PermanentAddress = permanentAddress;
            BankName = bankName;
            BankAccount = bankAccount;
            FullNameBangla = fullNameBangla;
            NameofFather = nameofFather;
            NameofSpouce = nameofSpouce;
            NameofMother = nameofMother;
            PlanetEmployeeID = planetEmployeeID;
            Position = position;
            ErrorDescription = errorDescription;
            FileName = fileName;
            JobType = jobType;
            PermanentDate = permanentDate;
            Emailaddress = emailaddress;
            EmergencyContact = emergencyContact;
            PermanentCountry = permanentCountry;
            PermanentDistrict = permanentDistrict;
            PermanentThana = permanentThana;
            PermanentPostOffice = permanentPostOffice;
            PermanentVillage = permanentVillage;
            PermanentAddressLine1 = permanentAddressLine1;
            PermanentAddressLine2 = permanentAddressLine2;
            PresentCountry = presentCountry;
            PresentDistrict = presentDistrict;
            PresentThana = presentThana;
            PresentPostOffice = presentPostOffice;
            PresentVillage = presentVillage;
            PresentAddressLine1 = presentAddressLine1;
            PresentAddressLine2 = presentAddressLine2;
            Grade = grade;
        }


        //public void MarkAsDeleteEployeeRawDataPrep()
        //{
        //    IsDeleted = true;
        //}


    }
}

