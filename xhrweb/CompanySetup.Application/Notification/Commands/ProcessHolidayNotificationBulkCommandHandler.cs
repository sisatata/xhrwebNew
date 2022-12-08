using CompanySetup.Core.Entities.NotificationAggregate;
using CompanySetup.Core.Interfaces;
using CompanySetup.Core.Specifications;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CompanyEntities = CompanySetup.Core.Entities;

namespace CompanySetup.Application.Notification.Commands
{
    public class ProcessHolidayNotificationBulkCommandHandler : IRequestHandler<Commands.V1.ProcessHolidayNotificationBulk, NotificationCommandVM>
    {
        public ProcessHolidayNotificationBulkCommandHandler(IAsyncRepository<CompanyEntities.Notification, Guid> repository,
            IAsyncRepository<CompanyEntities.CompanyAggregate.Company, Guid> repositoryCompany,
            IReferenceRepository<CompanyEntities.Holiday, Guid> repositoryHoliday,
            IReferenceRepository<CompanyEntities.EmployeeDevice, Guid> repositoryEmployeeDevice,
            INotificationBulkRepository notificationBulkRepository)
        {
            _repository = repository;
            _repositoryCompany = repositoryCompany;
            _repositoryHoliday = repositoryHoliday;
            _repositoryEmployeeDevice = repositoryEmployeeDevice;
            _notificationBulkRepository = notificationBulkRepository;
        }

        private readonly IAsyncRepository<CompanyEntities.Notification, Guid> _repository;
        private readonly IAsyncRepository<CompanyEntities.CompanyAggregate.Company, Guid> _repositoryCompany;
        private readonly IReferenceRepository<CompanyEntities.Holiday, Guid> _repositoryHoliday;
        private readonly IReferenceRepository<CompanyEntities.EmployeeDevice, Guid> _repositoryEmployeeDevice;
        private readonly INotificationBulkRepository _notificationBulkRepository;
        public async Task<NotificationCommandVM> Handle(Commands.V1.ProcessHolidayNotificationBulk request, CancellationToken cancellationToken)
        {
            var response = new NotificationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var holidayDate = request.ProcessDate;

                var companyActiveFilter = new CompanyActiveFilterSpecification();
                var companyActiveList = await _repositoryCompany.ListAsync(companyActiveFilter).ConfigureAwait(false);
                if (request.CompanyId != null || request.CompanyId != Guid.Empty)
                {
                    companyActiveList = companyActiveList.ToList().FindAll(x => x.Id == request.CompanyId);
                }
                var holidayFilterByDate = new HolidayFilterByDateSpecification(holidayDate);
                var holidays = await _repositoryHoliday.ListAsync(holidayFilterByDate).ConfigureAwait(false);

                var employeeDevceFilter = new EmployeeDevceFilterSpecification();
                var employeeDevceList = await _repositoryEmployeeDevice.ListAsync(employeeDevceFilter).ConfigureAwait(false);

                var holidayNotificationFilter = new HolidayNotificationFilterSpecification();
                var holidayNotificationList = await _repository.ListAsync(holidayNotificationFilter).ConfigureAwait(false);

                var notificationAggregate = new NotificationAggregate(companyActiveList, holidays,
                    employeeDevceList, holidayNotificationList.ToList());

                notificationAggregate.StartHolidayNotificationBulk();

                await _notificationBulkRepository.InsertUpdate(notificationAggregate).ConfigureAwait(false);

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
