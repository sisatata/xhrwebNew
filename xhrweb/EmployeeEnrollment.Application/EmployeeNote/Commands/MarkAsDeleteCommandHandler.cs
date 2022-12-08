using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeNote.Commands
{
    //public class MarkAsDeleteCommandHandler : IRequestHandler<Commands.V1.MarkAsDeleteEmployeeNote, EmployeeNoteCommandVM>
    //{
    //    public MarkAsDeleteCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeNote, Guid> repository)
    //    {
    //        _repository = repository;
    //    }

    //    private readonly IAsyncRepository<EmployeeEntities.EmployeeNote, Guid> _repository;

    //    public async Task<EmployeeNoteCommandVM>
    //        Handle(Commands.V1.MarkAsDeleteEmployeeNote request, CancellationToken cancellationToken)
    //    {
    //        var response = new EmployeeNoteCommandVM
    //        {
    //            Status = false,
    //            Message = "error"
    //        };

    //        try
    //        {
    //            var entity = await _repository.GetByIdAsync(request.Id);
    //            entity.MarkAsDelete();

    //            await _repository.UpdateAsync(entity);

    //            response.Status = true;
    //            response.Message = "entity deleted successfully.";
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
