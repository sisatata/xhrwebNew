using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeRawDataPrep.Commands
{
    public class StartEmployeeRawDataProcessingCommandHandler : IRequestHandler<Commands.V1.StartEmployeeRawDataProcessing, EmployeeRawDataPrepCommandVM>
    {
        public StartEmployeeRawDataProcessingCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeRawDataPrep, Int64>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeRawDataPrep, Int64>
        _repository;

        public async Task<EmployeeRawDataPrepCommandVM> Handle(Commands.V1.StartEmployeeRawDataProcessing request, CancellationToken cancellationToken)
        {
            var response = new EmployeeRawDataPrepCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {

                //------
                using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(request.FileName)))
                {
                    var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
                    var totalRows = myWorksheet.Dimension.End.Row;
                    var totalColumns = myWorksheet.Dimension.End.Column;
                }

                    //=====================
                    //var entity = EmployeeEntities.EmployeeRawDataPrep.Create(
                    //         request.EmployeeCode,
                    //         request.Gross,
                    //         request.FullName,
                    //         request.NID,
                    //         request.BID,
                    //         request.MobileNo,
                    //         request.JoiningDate,
                    //         request.DOB,
                    //         request.Gender,
                    //         request.Nationality,
                    //         request.NightBill,
                    //         request.OT,
                    //         request.Religion,
                    //         request.Division,
                    //         request.Department,
                    //         request.Section,
                    //         request.StaffCategory,
                    //         request.Company,
                    //         request.Country,
                    //         request.MaritalStatus,
                    //         request.BloodGroup,
                    //         request.PresentAddress,
                    //         request.PermanentAddress,
                    //         request.BankName,
                    //         request.BankAccount,
                    //         request.FullNameBangla,
                    //         request.NameofFather,
                    //         request.NameofSpouce,
                    //         request.NameofMother,
                    //         request.planetEmployeeID,
                    //         request.Position,
                    //         request.ErrorDescription,
                    //         request.FileName,
                    //         request.JobType,
                    //         request.PermanentDate,
                    //         request.Emailaddress,
                    //         request.EmergencyContact,
                    //         request.PermanentDistrict,
                    //         request.PermanentThana,
                    //         request.PermanentPostOffice,
                    //         request.PermanentVillage,
                    //         request.PermanentAddressLine1,
                    //         request.PermanentAddressLine2,
                    //         request.PresentDistrict,
                    //         request.PresentThana,
                    //         request.PresentPostOffice,
                    //         request.PresentVillage,
                    //         request.PresentAddressLine1,
                    //         request.PresentAddressLine2



                    //);
                    //var data = await _repository.AddAsync(entity);

                response.Status = true;
                response.Message = "entity created successfully.";
                //response.Id = entity.Id;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
