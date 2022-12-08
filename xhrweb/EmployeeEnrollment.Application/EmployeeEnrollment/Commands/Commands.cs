using ASL.Hrms.SharedKernel.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeEnrollment.Commands
{
    public static class Commands
    {
        public static class V1
        {
            public class CreateEmployeeEnrollment : IRequest<EmployeeEnrollmentCommandVM>
            {
                public Guid EmployeeId { get; set; }
                public DateTime JoiningDate { get; set; }
                public DateTime? QuitDate { get; set; }
                public Int16 StatusId { get; set; }
                public DateTime? PermanentDate { get; set; }
                public Guid? JobTypeId { get; set; }
                public Guid? GradeId { get; set; }
                public Guid? SubGradeId { get; set; }
                public Guid? EmployeeTypeId { get; set; }
                public Guid? EmployeeCategoryId { get; set; }
                public Guid? ConfirmationId { get; set; }
                public Guid? GenderId { get; set; }
                public string LeaveTypeGroup { get; set; }
            }

            public class UpdateEmployeeEnrollment : IRequest<EmployeeEnrollmentCommandVM>
            {
                public Guid Id { get; set; }
                public Guid EmployeeId { get; set; }
                public DateTime JoiningDate { get; set; }
                public DateTime? QuitDate { get; set; }
                public Int16 StatusId { get; set; }
                public DateTime? PermanentDate { get; set; }
                public Guid? JobTypeId { get; set; }
                public Guid? GradeId { get; set; }
                public Guid? SubGradeId { get; set; }
                public Guid? EmployeeTypeId { get; set; }
                public Guid? EmployeeCategoryId { get; set; }
                public Guid? ConfirmationId { get; set; }
                public Guid? GenderId { get; set; }
                public string LeaveTypeGroup { get; set; }
            }

            public class MarkAsDeleteEmployeeEnrollment : IRequest<EmployeeEnrollmentCommandVM>
            {
                public Guid Id { get; set; }
            }

            public class UpdateProfilePicture : IRequest<UpdateEmployeeImageCommandVM>
            {
                public Guid Id { get; set; }
                public Guid EmployeeId { get; set; }
                public IFormFile EmployeeImage { get; set; }

                public PlanetHRSettings Settings { get; set; }
            }

            public class UpdateSignature : IRequest<UpdateEmployeeSignatureCommandVM>
            {
                public Guid Id { get; set; }
                public Guid EmployeeId { get; set; }
                public IFormFile EmployeeSignature { get; set; }
                public PlanetHRSettings Settings { get; set; }
            }

            public class UpdateStatusEmployee : IRequest<EmployeeEnrollmentCommandVM>
            {
                public Guid Id { get; set; }
                public Int16 StatusId { get; set; }
            }
        }
    }
}
