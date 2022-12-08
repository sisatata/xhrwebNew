using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeRawDataHist : BaseEntity<Int64>, IAggregateRoot
    {
        //public Int64? ID {get; private set;}
        public string EmployeeCode { get; private set; }
        public string Gross { get; private set; }
        public string FullName { get; private set; }
        public string NID { get; private set; }
        public string BID { get; private set; }
        public string MobileNo { get; private set; }
        public string JoiningDate { get; private set; }
        public string DOB { get; private set; }
        public string Gender { get; private set; }
        public string Nationality { get; private set; }
        public string NightBill { get; private set; }
        public string OT { get; private set; }
        public string Religion { get; private set; }
        public string Division { get; private set; }
        public string Department { get; private set; }
        public string Section { get; private set; }
        public string StaffCategory { get; private set; }
        public string Company { get; private set; }
        public string Country { get; private set; }
        public string MaritalStatus { get; private set; }
        public string Status { get; private set; }
        public string BloodGroup { get; private set; }
        public string PresentAddress { get; private set; }
        public string PermanentAddress { get; private set; }
        public string BankName { get; private set; }
        public string BankAccount { get; private set; }
        public string FullNameBangla { get; private set; }
        public string NameofFather { get; private set; }
        public string NameofSpouce { get; private set; }
        public string NameofMother { get; private set; }
        public Guid? planetEmployeeID { get; private set; }
        public string Position { get; private set; }
        public string ErrorDescription { get; private set; }
        public string FileName { get; private set; }
        public DateTime MoveDate { get; private set; }
        public string JobType { get; private set; }
        public string PermanentDate { get; private set; }
        public string Emailaddress { get; private set; }
        public string EmergencyContact { get; private set; }
        public string PermanentDistrict { get; private set; }
        public string PermanentThana { get; private set; }
        public string PermanentPostOffice { get; private set; }
        public string PermanentVillage { get; private set; }
        public string PermanentAddressLine1 { get; private set; }
        public string PermanentAddressLine2 { get; private set; }
        public string PresentDistrict { get; private set; }
        public string PresentThana { get; private set; }
        public string PresentPostOffice { get; private set; }
        public string PresentVillage { get; private set; }
        public string PresentAddressLine1 { get; private set; }
        public string PresentAddressLine2 { get; private set; }

        // not persisted
        public TrackingState State { get; set; }
        public EmployeeRawDataHist(Int64 id) : base(id) { }
        private EmployeeRawDataHist() : base(0) { }

        public static EmployeeRawDataHist Create(

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
         string division,
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
         DateTime moveDate,
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
         string presentAddressLine2
        )

        {
            var oModel = new EmployeeRawDataHist();

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
            oModel.Division = division;
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
            oModel.planetEmployeeID = planetEmployeeID;
            oModel.Position = position;
            oModel.ErrorDescription = errorDescription;
            oModel.FileName = fileName;
            oModel.MoveDate = moveDate;
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

            return oModel;

        }


        public void UpdateEmployeeRawDataHist
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
         string division,
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
         DateTime moveDate,
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
         string presentAddressLine2
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
            Division = division;
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
            planetEmployeeID = planetEmployeeID;
            Position = position;
            ErrorDescription = errorDescription;
            FileName = fileName;
            MoveDate = moveDate;
            JobType = jobType;
            PermanentDate = permanentDate;
            emailaddress = emailaddress;
            EmergencyContact = emergencyContact;
            PermanentDistrict = permanentDistrict;
            PermanentThana = permanentThana;
            PermanentPostOffice = permanentPostOffice;
            PermanentVillage = permanentVillage;
            PermanentAddressLine1 = permanentAddressLine1;
            PermanentAddressLine2 = permanentAddressLine2;
            PresentDistrict = presentDistrict;
            PresentThana = presentThana;
            PresentPostOffice = presentPostOffice;
            PresentVillage = presentVillage;
            PresentAddressLine1 = presentAddressLine1;
            PresentAddressLine2 = presentAddressLine2;
        }


        //public void MarkAsDeleteEployeeRawDataHist ()
        //{
        //IsDeleted = true;
        //}


    }
}

