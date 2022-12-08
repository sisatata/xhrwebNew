using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.Bank.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateBank, BankCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.Bank, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.Bank, Guid>
        _repository;

        public async Task<BankCommandVM> Handle(Commands.V1.CreateBank request, CancellationToken cancellationToken)
        {
            var response = new BankCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.Bank.Create(
                         request.BankName,
                         request.BankNameLC,
                         request.SortOrder,
                         request.CompanyId,
                         request.IsDeleted,
                         request.PaymentOptionId
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
