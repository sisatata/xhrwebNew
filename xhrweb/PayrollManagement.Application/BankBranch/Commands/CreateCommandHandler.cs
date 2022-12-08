using PayrollManagement.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = PayrollManagement.Core.Entities;

namespace PayrollManagement.Application.BankBranch.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateBankBranch, BankBranchCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.BankBranch, Guid>
        repository)
        {
            _repository = repository;
        }

        private readonly IAsyncRepository<EmployeeEntities.BankBranch, Guid>
        _repository;

        public async Task<BankBranchCommandVM> Handle(Commands.V1.CreateBankBranch request, CancellationToken cancellationToken)
        {
            var response = new BankBranchCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var entity = EmployeeEntities.BankBranch.Create(
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
