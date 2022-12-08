//using PayrollManagement.Core.Interfaces;
//using MediatR;
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using BonusEntities = PayrollManagement.Core.Entities.EmployeeBonusProcessAggregate;

//namespace PayrollManagement.Application.EmployeeBonusProcessedData .Commands
//{
//    public class UpdateCommandHandler : IRequestHandler< Commands.V1. UpdateEmployeeBonusProcessedData, EmployeeBonusProcessedDataCommandVM>
//    {
//    public UpdateCommandHandler(IAsyncRepository<BonusEntities. EmployeeBonusProcessedData,Guid> repository)
//        {
//        _repository = repository;
//        }

//        private readonly IAsyncRepository<BonusEntities. EmployeeBonusProcessedData,Guid> _repository;

//            public async Task< EmployeeBonusProcessedDataCommandVM>
//                Handle(Commands.V1. UpdateEmployeeBonusProcessedData request, CancellationToken cancellationToken)
//                {
//                var response = new EmployeeBonusProcessedDataCommandVM
//                {
//                Status = false,
//                Message = "error"
//                };

//                try
//                {
//                var entity = await _repository.GetByIdAsync(request.Id);
//                entity. UpdateEmployeeBonusProcessedData (
                                 
//                         request. EmployeeId, 
//                         request. BonusTypeID, 
//                         request. BonusDate, 
//                         request. FinancialYear, 
//                         request. PaymentOptionId, 
//                         request. CompanyId, 
//                         request. BranchId, 
//                         request. DepartmentId, 
//                         request. PositionID, 
//                         request. JoiningDate, 
//                         request. GradeID, 
//                         request. Basic, 
//                         request. HouseRent, 
//                         request. Medical, 
//                         request. Conveyance, 
//                         request. Food, 
//                         request. GrossSalary, 
//                         request. PayableBonus, 
//                         request. TaxDeductedAmount, 
//                         request. NetPayableBonus, 
//                         request. Basic_V2, 
//                         request. HouseRent_V2, 
//                         request. GrossSalary_V2, 
//                         request. PayableBonus_V2, 
//                         request. TaxDeductedAmount_V2, 
//                         request. NetPayableBonus_V2, 
//                         request. Remarks, 
//                         request. IsApproved, 
//                         request. ApprovedBy, 
//                         request. ApprovedTime, 
//                         request. IsDeleted, 
//                         request. BonusConfigurationId 

//    );

//                await _repository.UpdateAsync(entity);

//                response.Status = true;
//                response.Message = "entity updated successfully.";
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

