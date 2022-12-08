using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EployeeRawDataHist .Commands
{
    //public class MarkAsDeleteCommandHandler : IRequestHandler< Commands.V1. MarkAsDeleteEployeeRawDataHist, EmployeeRawDataHistCommandVM>
    //{
    //public MarkAsDeleteCommandHandler(IAsyncRepository< EmployeeEntities. EployeeRawDataHist,Guid> repository)
    //    {
    //    _repository = repository;
    //    }

    //    private readonly IAsyncRepository< EmployeeEntities. EployeeRawDataHist,Guid> _repository;

    //        public async Task< EmployeeRawDataHistCommandVM>
    //            Handle( Commands.V1. MarkAsDeleteEployeeRawDataHist request, CancellationToken cancellationToken)
    //            {
    //            var response = new EmployeeRawDataHistCommandVM
    //            {
    //            Status = false,
    //            Message = "error"
    //            };

    //            try
    //            {
    //            var entity = await _repository.GetByIdAsync(request.Id);
    //            //entity.MarkAsDelete();

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
