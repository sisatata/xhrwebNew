using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeRawDataPrep.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeRawDataPrep, EmployeeRawDataPrepCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeRawDataPrep, Int64> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeRawDataPrep, Int64> _repository;

        public async Task<EmployeeRawDataPrepCommandVM>
            Handle(Commands.V1.UpdateEmployeeRawDataPrep request, CancellationToken cancellationToken)
        {
            var response = new EmployeeRawDataPrepCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.ID.Value);
    //            entity.UpdateEmployeeRawDataPrep(

    //                     request.EmployeeCode,
    //                     request.Gross,
    //                     request.FullName,
    //                     request.NID,
    //                     request.BID,
    //                     request.MobileNo,
    //                     request.JoiningDate,
    //                     request.DOB,
    //                     request.Gender,
    //                     request.Nationality,
    //                     request.NightBill,
    //                     request.OT,
    //                     request.Religion,
    //                     request.Division,
    //                     request.Department,
    //                     request.Section,
    //                     request.StaffCategory,
    //                     request.Company,
    //                     request.Country,
    //                     request.MaritalStatus,
    //                     request.BloodGroup,
    //                     request.PresentAddress,
    //                     request.PermanentAddress,
    //                     request.BankName,
    //                     request.BankAccount,
    //                     request.FullNameBangla,
    //                     request.NameofFather,
    //                     request.NameofSpouce,
    //                     request.NameofMother,
    //                     request.planetEmployeeID,
    //                     request.Position,
    //                     request.ErrorDescription,
    //                     request.FileName,
    //                     request.JobType,
    //                     request.PermanentDate,
    //                     request.Emailaddress,
    //                     request.EmergencyContact,
    //                     request.PermanentDistrict,
    //                     request.PermanentThana,
    //                     request.PermanentPostOffice,
    //                     request.PermanentVillage,
    //                     request.PermanentAddressLine1,
    //                     request.PermanentAddressLine2,
    //                     request.PresentDistrict,
    //                     request.PresentThana,
    //                     request.PresentPostOffice,
    //                     request.PresentVillage,
    //                     request.PresentAddressLine1,
    //                     request.PresentAddressLine2

    //);

                await _repository.UpdateAsync(entity);

                response.Status = true;
                response.Message = "entity updated successfully.";
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

