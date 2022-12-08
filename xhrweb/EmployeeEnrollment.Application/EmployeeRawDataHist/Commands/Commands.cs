using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeRawDataHist.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeRawDataHist : IRequest<EmployeeRawDataHistCommandVM>
            {
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
                public string Division { get; set; }
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
                public Guid? planetEmployeeID { get; set; }
                public string Position { get; set; }
                public string ErrorDescription { get; set; }
                public string FileName { get; set; }
                public DateTime MoveDate { get; set; }
                public string JobType { get; set; }
                public string PermanentDate { get; set; }
                public string Emailaddress { get; set; }
                public string EmergencyContact { get; set; }
                public string PermanentDistrict { get; set; }
                public string PermanentThana { get; set; }
                public string PermanentPostOffice { get; set; }
                public string PermanentVillage { get; set; }
                public string PermanentAddressLine1 { get; set; }
                public string PermanentAddressLine2 { get; set; }
                public string PresentDistrict { get; set; }
                public string PresentThana { get; set; }
                public string PresentPostOffice { get; set; }
                public string PresentVillage { get; set; }
                public string PresentAddressLine1 { get; set; }
                public string PresentAddressLine2 { get; set; }
            }

            public class UpdateEmployeeRawDataHist : IRequest<EmployeeRawDataHistCommandVM>
            {
                public Int64? ID { get; set; }
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
                public string Division { get; set; }
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
                public Guid? planetEmployeeID { get; set; }
                public string Position { get; set; }
                public string ErrorDescription { get; set; }
                public string FileName { get; set; }
                public DateTime MoveDate { get; set; }
                public string JobType { get; set; }
                public string PermanentDate { get; set; }
                public string Emailaddress { get; set; }
                public string EmergencyContact { get; set; }
                public string PermanentDistrict { get; set; }
                public string PermanentThana { get; set; }
                public string PermanentPostOffice { get; set; }
                public string PermanentVillage { get; set; }
                public string PermanentAddressLine1 { get; set; }
                public string PermanentAddressLine2 { get; set; }
                public string PresentDistrict { get; set; }
                public string PresentThana { get; set; }
                public string PresentPostOffice { get; set; }
                public string PresentVillage { get; set; }
                public string PresentAddressLine1 { get; set; }
                public string PresentAddressLine2 { get; set; }
            }

            public class MarkAsDeleteEployeeRawDataHist : IRequest<EmployeeRawDataHistCommandVM>
            {
                public Guid Id { get; set; }
            }
        }
    }
}
