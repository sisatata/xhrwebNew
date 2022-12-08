using Attendance.Application.ShiftAllocation.Commands.Models;
using Attendance.Core.Entities.ShiftAllocationAggregate;
using Attendance.Core.Interfaces;
using Attendance.Core.Specifications;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Attendance.Core.Entities.ProcessShiftAllocationAggregate;

namespace Attendance.Application.ShiftAllocation.Commands
{
    public class ProcessShiftAllocationBulkCommandHandler : IRequestHandler<ShiftAllocationCommands.V1.ProcessShiftAllocationBulk, ShiftAllocationCommandVM>
    {


        public ProcessShiftAllocationBulkCommandHandler(IAsyncRepository<Core.Entities.ShiftAllocationAggregate.ShiftAllocation, Guid> repository,
            IAsyncRepository<Core.Entities.ShiftGroup, Guid> repositoryShiftGroup,
            IAsyncRepository<Core.Entities.Shift, Guid> repositoryShift,
            IReferenceRepository<Core.Entities.Company, Guid> repositoryCompany,
            IReferenceRepository<Core.Entities.Employee, Guid> repositoryEmployee,
            IReferenceRepository<Core.Entities.AttendanceCalendar, Guid> repositoryFinancialYear,
            IReferenceRepository<Core.Entities.MonthCycle, Guid> repositoryMonthCycle,
            IReferenceRepository<Core.Entities.EmployeeCustomConfiguration, Guid> repositoryEmployeeCustomConfiguration,
            IShiftAllocationProcessRepository shiftAllocationProcessRepository)
        {
            _repository = repository;
            _repositoryShiftGroup = repositoryShiftGroup;
            _repositoryShift = repositoryShift;
            _repositoryCompany = repositoryCompany;
            _repositoryEmployee = repositoryEmployee;
            _repositoryFinancialYear = repositoryFinancialYear;
            _repositoryMonthCycle = repositoryMonthCycle;
            _repositoryEmployeeCustomConfiguration = repositoryEmployeeCustomConfiguration;
            _shiftAllocationProcessRepository = shiftAllocationProcessRepository;
        }
        private readonly IAsyncRepository<Core.Entities.ShiftAllocationAggregate.ShiftAllocation, Guid> _repository;
        private readonly IAsyncRepository<Core.Entities.ShiftGroup, Guid> _repositoryShiftGroup;
        private readonly IAsyncRepository<Core.Entities.Shift, Guid> _repositoryShift;

        private readonly IReferenceRepository<Core.Entities.Company, Guid> _repositoryCompany;
        private readonly IReferenceRepository<Core.Entities.Employee, Guid> _repositoryEmployee;
        private readonly IReferenceRepository<Core.Entities.AttendanceCalendar, Guid> _repositoryFinancialYear;
        private readonly IReferenceRepository<Core.Entities.MonthCycle, Guid> _repositoryMonthCycle;
        private readonly IReferenceRepository<Core.Entities.EmployeeCustomConfiguration, Guid> _repositoryEmployeeCustomConfiguration;

        private readonly IShiftAllocationProcessRepository _shiftAllocationProcessRepository;

        public async Task<ShiftAllocationCommandVM> Handle(ShiftAllocationCommands.V1.ProcessShiftAllocationBulk request, CancellationToken cancellationToken)
        {
            var response = new ShiftAllocationCommandVM
            {
                Status = false,
                Message = "error"
            };

            try
            {
                var companyActiveFilter = new CompanyActiveFilterSpecification();
                var companyActiveList = await _repositoryCompany.ListAsync(companyActiveFilter).ConfigureAwait(false);

                if (request.CompanyId != null && request.CompanyId != Guid.Empty)
                {
                    companyActiveList = companyActiveList.ToList().FindAll(x => x.Id == request.CompanyId);
                }

                var employeeQuery = new EmployeeNoFilterSpecification(request.CompanyId);
                var employees = await _repositoryEmployee.ListAsync(employeeQuery).ConfigureAwait(false);

                var shiftAllocationFilter = new ShiftAllocationNoFilterSpecification();
                var shiftAllocations = await _repository.ListAsync(shiftAllocationFilter).ConfigureAwait(false);

                var shiftGroupQuery = new ShiftGroupFilterSpecification(request.CompanyId);
                var shiftGroups = await _repositoryShiftGroup.ListAsync(shiftGroupQuery).ConfigureAwait(false);

                var shiftQuery = new ShiftFilterByShiftGroupIdSpecification(request.CompanyId, null);
                var shifts = await _repositoryShift.ListAsync(shiftQuery).ConfigureAwait(false);

                var attendanceYearQuery = new AttendanceCalenderNoFilterSpecification(request.CompanyId);
                var attendanceYears = await _repositoryFinancialYear.ListAsync(attendanceYearQuery).ConfigureAwait(false);

                var monthCycleQuery = new MonthCycleNoFilterSpecification();
                var monthCycles = await _repositoryMonthCycle.ListAsync(monthCycleQuery).ConfigureAwait(false);

                var configurationQuery = new EmployeeCustomConfigurationFilterSpecification("DefaultShiftGroup");
                var employeeConfigurations = await _repositoryEmployeeCustomConfiguration.ListAsync(configurationQuery).ConfigureAwait(false);

                var processShiftAllocationAggregate = new ProcessShiftAllocationAggregate(companyActiveList,
                    employees,
                    shiftGroups,
                    shifts,
                    attendanceYears,
                    monthCycles,
                    shiftAllocations,
                    employeeConfigurations
                    );

                processShiftAllocationAggregate.StartShiftAllocationProcess();
                await _shiftAllocationProcessRepository.SaveShiftAllocation(processShiftAllocationAggregate).ConfigureAwait(false);
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
