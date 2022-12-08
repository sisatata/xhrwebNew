using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using PayrollEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.BankBranch.Commands
{
    public class UpdateCommandHandler : IRequestHandler<Commands.V1.UpdateBankBranch, BankBranchCommandVM>
    {
        public UpdateCommandHandler(IAsyncRepository<PayrollEntities.BankBranch, Guid> repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<PayrollEntities.BankBranch, Guid> _repository;

        public async Task<BankBranchCommandVM>
            Handle(Commands.V1.UpdateBankBranch request, CancellationToken cancellationToken)
        {
            var response = new BankBranchCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = await _repository.GetByIdAsync(request.Id.Value);
                entity.UpdateBankBranch(
                         request.BranchName,
                         request.BranchNameLC,
                         request.BranchAddress,
                         request.ContactPerson,
                         request.ContactNumber,
                         request.ContactEmailId,
                         request.SortOrder,
                         request.CompanyId,
                         request.BankId

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

