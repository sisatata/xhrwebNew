using EmployeeEnrollment.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeCard.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateEmployeeCard, EmployeeCardCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeCard, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeCard, Guid> _repository;

        public async Task<EmployeeCardCommandVM>
            Handle(Commands.V1.UpdateEmployeeCard request, CancellationToken cancellationToken)
        {
            var response = new EmployeeCardCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id);
                entity.UpdateEmployeeCard(
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

