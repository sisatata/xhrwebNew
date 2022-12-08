using ASL.Hrms.SharedKernel.Interfaces;
using LeaveManagement.Core.Entities;
using LeaveManagement.Core.Events;
using LeaveManagement.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using static ASL.Hrms.SharedKernel.DomainEventDispatcher;

namespace LeaveManagement.Core.Handlers.DomainEventHandlers
{
    public class LeaveAppliedEventHandler : IHandle<LeaveAppliedEvent>
    {
        public LeaveAppliedEventHandler(IServiceBus serviceBus, IConfiguration configuration,
            IReferenceRepository<Employee, Guid> employeeRepository)
        {
            _serviceBus = serviceBus;
            _configuration = configuration;
            _employeeRepository = employeeRepository;
        }

        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly IReferenceRepository<Employee, Guid> _employeeRepository;
        public async Task Handle(LeaveAppliedEvent domainEvent)
        {
            var employee = await _employeeRepository.GetByIdAsync(domainEvent.LeaveApplied.EmployeeId);
            var @event = new ApplicationEvents.LeaveAppliedEvent
            {
                EmployeeId = employee.Id,
                EmployeeName = employee.FullName,
                ApplicationId = domainEvent.LeaveApplied.Id,
                NoOfDays = domainEvent.LeaveApplied.NoOfDays
            };

            await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false);
            
            // send push notification
        }
    }
}
