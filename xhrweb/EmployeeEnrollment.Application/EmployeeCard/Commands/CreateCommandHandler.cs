using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeCard.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeCard, EmployeeCardCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeCard, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeCard, Guid>
        _repository;

        public async Task<EmployeeCardCommandVM> Handle(Commands.V1.CreateEmployeeCard request, CancellationToken cancellationToken)
        {
            var response = new EmployeeCardCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.EmployeeCard.Create(
                         request.EmployeeId,
                         request.CardMaskingValue,
                         request.IssueDate,
                         request.ChargeAmount,
                         request.IsPaid,
                         request.PaymentDate,
                         request.CardRevoked,
                         request.CardRevokedDate,
                         request.CompanyId,
                         request.IsDeleted
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
