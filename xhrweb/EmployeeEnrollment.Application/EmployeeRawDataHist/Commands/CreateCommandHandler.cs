using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeRawDataHist.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeRawDataHist, EmployeeRawDataHistCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeRawDataHist, Int64> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeRawDataHist, Int64> _repository;

        public async Task<EmployeeRawDataHistCommandVM> Handle(Commands.V1.CreateEmployeeRawDataHist request, CancellationToken cancellationToken)
        {
            var response = new EmployeeRawDataHistCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.EmployeeRawDataHist.Create(
                         request.EmployeeCode,
                         request.Gross,
                         request.FullName,
                         request.NID,
                         request.BID,
                         request.MobileNo,
                         request.JoiningDate,
                         request.DOB,
                         request.Gender,
                         request.Nationality,
                         request.NightBill,
                         request.OT,
                         request.Religion,
                         request.Division,
                         request.Department,
                         request.Section,
                         request.StaffCategory,
                         request.Company,
                         request.Country,
                         request.MaritalStatus,
                         request.BloodGroup,
                         request.PresentAddress,
                         request.PermanentAddress,
                         request.BankName,
                         request.BankAccount,
                         request.FullNameBangla,
                         request.NameofFather,
                         request.NameofSpouce,
                         request.NameofMother,
                         request.planetEmployeeID,
                         request.Position,
                         request.ErrorDescription,
                         request.FileName,
                         request.MoveDate,
                         request.JobType,
                         request.PermanentDate,
                         request.Emailaddress,
                         request.EmergencyContact,
                         request.PermanentDistrict,
                         request.PermanentThana,
                         request.PermanentPostOffice,
                         request.PermanentVillage,
                         request.PermanentAddressLine1,
                         request.PermanentAddressLine2,
                         request.PresentDistrict,
                         request.PresentThana,
                         request.PresentPostOffice,
                         request.PresentVillage,
                         request.PresentAddressLine1,
                         request.PresentAddressLine2

                );
                var data = await _repository.AddAsync(entity);

                response.Status = true;
                response.Message = "entity created successfully.";
                response.Id = entity.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
