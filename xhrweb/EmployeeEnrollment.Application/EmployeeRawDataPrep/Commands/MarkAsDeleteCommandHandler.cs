using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EployeeRawDataPrep .Commands
{
    //public class MarkAsDeleteCommandHandler : IRequestHandler< Commands.V1. MarkAsDeleteEployeeRawDataPrep, EployeeRawDataPrepCommandVM>
    //{
    //public MarkAsDeleteCommandHandler(IAsyncRepository< EmployeeEntities. EmployeeRawDataPrep,Guid> repository)
    //    {
    //    _repository = repository;
    //    }

    //    private readonly IAsyncRepository< EmployeeEntities. EmployeeRawDataPrep,Guid> _repository;

    //        public async Task< EployeeRawDataPrepCommandVM>
    //            Handle( Commands.V1. MarkAsDeleteEployeeRawDataPrep request, CancellationToken cancellationToken)
    //            {
    //            var response = new EployeeRawDataPrepCommandVM
    //            {
    //            Status = false,
    //            Message = "error"
    //            };

    //            try
    //            {
    //            var entity = await _repository.GetByIdAsync(request.Id);
    //            entity.MarkAsDelete();

    //            await _repository.UpdateAsync(entity);

    //            response.Status = true;
    //            response.Message = "entity deleted successfully.";
    //            response.Id = entity.Id;
    //            }
    //            catch (Exception ex)
    //            {
    //            response.Message = ex.Message;
    //            }

    //            return response;
    //            }
    //            }
                }
