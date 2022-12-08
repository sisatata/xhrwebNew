//using EmployeeEnrollment.Core.Interfaces;
//using MediatR;
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using EmployeeEntities = EmployeeEnrollment.Core.Entities;

//namespace EmployeeEnrollment.Application.EmployeeBonusProcessedData .Commands
//{
//    public class MarkAsDeleteCommandHandler : IRequestHandler< Commands.V1. MarkAsDeleteEmployeeBonusProcessedData, EmployeeBonusProcessedDataCommandVM>
//    {
//    public MarkAsDeleteCommandHandler(IAsyncRepository< EmployeeEntities. EmployeeBonusProcessedData,Guid> repository)
//        {
//        _repository = repository;
//        }

//        private readonly IAsyncRepository< EmployeeEntities. EmployeeBonusProcessedData,Guid> _repository;

//            public async Task< EmployeeBonusProcessedDataCommandVM>
//                Handle( Commands.V1. MarkAsDeleteEmployeeBonusProcessedData request, CancellationToken cancellationToken)
//                {
//                var response = new EmployeeBonusProcessedDataCommandVM
//                {
//                Status = false,
//                Message = "error"
//                };

//                try
//                {
//                var entity = await _repository.GetByIdAsync(request.Id);
//                entity.MarkAsDelete();

//                await _repository.UpdateAsync(entity);

//                response.Status = true;
//                response.Message = "entity deleted successfully.";
//                response.Id = entity.Id;
//                }
//                catch (Exception ex)
//                {
//                response.Message = ex.Message;
//                }

//                return response;
//                }
//                }
//                }
