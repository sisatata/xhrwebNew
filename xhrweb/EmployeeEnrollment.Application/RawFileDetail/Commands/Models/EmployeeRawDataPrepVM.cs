using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.RawFileDetail.Commands.Models
{
    public class EmployeeRawDataPrepVM
    {
        public string Status { get; set; }
        public string ErrorDescription { get; set; }
        public Guid PlanetEmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string JoiningDate { get; set; }
        public string ConfirmationRuleName { get; set; }
        public string PermanentDate { get; set; }
        public string Grade { get; set; }
        public string LeaveTypeGroupName { get; set; }
        public string Gross { get; set; }
        public string SalaryStructureName { get; set; }
        public string PaymentOptionName { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string NameofFather { get; set; }
        public string NameofSpouce { get; set; }
        public string NameofMother { get; set; }
        public string DOB { get; set; }
        public string NID { get; set; }
        public string BID { get; set; }
        public string Emailaddress { get; set; }
        public string MobileNo { get; set; }
        public string EmergencyContact { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
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
    }
}
