using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;

namespace EmployeeEnrollment.Application.EmployeeStatus.Commands
{
    public class CreateCommandHandler : IRequestHandler<Commands.V1.CreateEmployeeStatus, EmployeeStatusCommandVM>
    {
        public CreateCommandHandler(IAsyncRepository<EmployeeEntities.EmployeeStatus, Guid> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        private readonly IAsyncRepository<EmployeeEntities.EmployeeStatus, Guid> _repository;
        private readonly IMediator _mediator;

        public async Task<EmployeeStatusCommandVM> Handle(Commands.V1.CreateEmployeeStatus request, CancellationToken cancellationToken)
        {
            var response = new EmployeeStatusCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var statusQuery = new EmployeeStatusByCompanyFilterSpecification(request.CompanyId);
                var statusList = await _repository.ListAsync(statusQuery).ConfigureAwait(false);
                var dbStatus = statusList.FirstOrDefault(x => x.IsDeleted == false && x.EmployeeStatusName.ToLower().Trim() == request.EmployeeStatusName.ToLower().Trim());
                if (dbStatus == null)
                {
                    Int16 newStatusId = (statusList == null || statusList.Count < 1) ? (Int16)1 : (Int16)(statusList.ToList().Max(r => r.StatusId) + 1);

                    var entity = EmployeeEntities.EmployeeStatus.Create(
                             request.EmployeeStatusName,
                             request.EmployeeStatusNameLC,
                             request.Rank,
                             request.CompanyId,
                             newStatusId
                    );
                    var data = await _repository.AddAsync(entity);
                    response.Id = entity.Id;
                }
                else
                {
                    var statusToUpdate = await _repository.GetByIdAsync(dbStatus.Id).ConfigureAwait(false);
                    statusToUpdate.UpdateEmployeeStatus(
                         request.EmployeeStatusName,
                         request.EmployeeStatusNameLC,
                         request.Rank,
                         request.CompanyId
                         );
                    await _repository.UpdateAsync(statusToUpdate);
                    response.Id = statusToUpdate.Id;
                }
                response.Status = true;
                response.Message = "entity created successfully.";

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
