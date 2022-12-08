using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;
using EmployeeEnrollment.Core.Interfaces;

namespace EmployeeEnrollment.Application.Employee.Commands
{
    //public class UpdateStatusEmployeeHandler : IRequestHandler<Commands.V1.UpdateStatusEmployee, EmployeeCommandVM>
    //{
    //    public UpdateStatusEmployeeHandler(IAsyncRepository<EmployeeEntities.Employee, Guid>
    //        repository)
    //    {
    //        _repository = repository;
    //    }

    //    private readonly IAsyncRepository<EmployeeEntities.Employee, Guid>
    //        _repository;

    //    public async Task<EmployeeCommandVM>
    //        Handle(Commands.V1.UpdateStatusEmployee request, CancellationToken cancellationToken)
    //    {
    //        var response = new EmployeeCommandVM
    //        {
    //            Status = false,
    //            Message = "error"
    //        };

    //        try
    //        {
    //            var entity = await _repository.GetByIdAsync(request.Id);
    //            entity.UpsertStatusId(request.StatusId);

    //            await _repository.UpdateAsync(entity);

    //            response.Status = true;
    //            response.Message = "Status Id updated successfully.";
    //            response.Id = entity.Id;
    //        }
    //        catch (Exception ex)
    //        {
    //            response.Message = ex.Message;
    //        }

    //        return response;
    //    }
    //}
}




