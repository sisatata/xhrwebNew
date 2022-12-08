using ASL.Hrms.SharedKernel.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using PayrollManagement.Application.BenefitBillClaim.Commands.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollManagement.Application.BenefitBillClaim.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateBenefitBillClaim : IRequest<BenefitBillClaimCommandVM>
            {
                public Guid? BenefitDeductionId { get; set; }
                public Guid? EmployeeId { get; set; }
                public DateTime? BillDate { get; set; }
                public DateTime? ClaimDate { get; set; }
                public decimal? AllocatedAmount { get; set; }
                public decimal? ClaimAmount { get; set; }
                public string Remarks { get; set; }
                //public decimal? ApprovedAmount { get; set; }
                //public Guid? ApprovedBy { get; set; }
                //public DateTime? ApprovedDate { get; set; }
                //public string ApprovedNotes { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
                public string Status { get; set; }
                public string LocationFrom { get;  set; }
                public string LocationTo { get;  set; }
                public Guid? VehicleTypeId { get;  set; }
                public int? NumberOfAttendees { get;  set; }
                public string NameOfAttendees { get;  set; }
                public IFormFile BillAttachment { get; set; }

                public PlanetHRSettings Settings { get; set; }
            }

            public class UpdateBenefitBillClaim : IRequest<BenefitBillClaimCommandVM>
            {
                public Guid Id { get; set; }
                public Guid? BenefitDeductionId { get; set; }
                public Guid? EmployeeId { get; set; }
                public DateTime? BillDate { get; set; }
                public DateTime? ClaimDate { get; set; }
                public decimal? AllocatedAmount { get; set; }
                public decimal? ClaimAmount { get; set; }
                public string Remarks { get; set; }
                //public decimal? ApprovedAmount { get; set; }
                //public Guid? ApprovedBy { get; set; }
                //public DateTime? ApprovedDate { get; set; }
                //public string ApprovedNotes { get; set; }
                public Boolean IsDeleted { get; set; }
                public Guid? CompanyId { get; set; }
                public string Status { get; set; }

                public string LocationFrom { get;  set; }
                public string LocationTo { get;  set; }
                public Guid? VehicleTypeId { get;  set; }
                public int? NumberOfAttendees { get;  set; }
                public string NameOfAttendees { get;  set; }
                public IFormFile BillAttachment { get; set; }

                public PlanetHRSettings Settings { get; set; }
            }

            public class ApproveBenefitBillClaim : IRequest<BenefitBillClaimCommandVM>
            {
                public Guid Id { get; set; }
                public decimal? ApprovedAmount { get; set; }
                public string ApprovedNotes { get; set; }
            }

            public class RejectBenefitBillClaim : IRequest<BenefitBillClaimCommandVM>
            {
                public Guid Id { get; set; }
                public string ApprovedNotes { get; set; }
            }
            public class MarkAsDeleteBenefitBillClaim : IRequest<BenefitBillClaimCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class MarkAsPaidBenefitBillClaim : IRequest<BenefitBillClaimCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class MarkAsSettledBenefitBillClaim : IRequest<BenefitBillClaimCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class UpdateBillAttachment : IRequest<UpdateBillAttachmentCommandVM>
            {
                public Guid Id { get; set; }
                //public Guid BenefitBillClaimI { get; set; }
                public IFormFile BillAttachment { get; set; }

                public PlanetHRSettings Settings { get; set; }
            }
        }
    }
}
