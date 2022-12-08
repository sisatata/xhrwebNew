using ASL.Hrms.SharedKernel.Enums;
using CompanySetup.Application.CommonLookUpType.Queries.Models;
using CompanySetup.Core.Entities;
using CompanySetup.Core.Interfaces;
using CompanySetup.Core.Specifications;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConfigurationEntities = CompanySetup.Core.Entities;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using TaskManagement.Core.Entities;
using ASL.Hrms.SharedKernel.Interfaces;

namespace CompanySetup.Application.CommonLookUpType.Queries
{
    public class GetCommonSettingsByCompanyHandler : IRequestHandler<Queries.GetCommonSettingsByCompany, CommonSettingsVM>
    {
        public GetCommonSettingsByCompanyHandler(DbConnection connection,
            IReferenceRepository<ConfigurationEntities.EmployeeWiseCustomConfiguration, Guid> employeeWiseCustomConfiguration, ICurrentUserContext userContext,
             IAsyncRepository<Core.Entities.Department, Guid> repository)
        {
            _connection = connection;
            _employeeWiseCustomConfiguration = employeeWiseCustomConfiguration;
            _userContext = userContext;
            _repository = repository;
        }
        private readonly DbConnection _connection;
        private IReferenceRepository<ConfigurationEntities.EmployeeWiseCustomConfiguration, Guid> _employeeWiseCustomConfiguration;
        private readonly ICurrentUserContext _userContext;
        private readonly IAsyncRepository<Core.Entities.Department, Guid> _repository;

        public async Task<CommonSettingsVM> Handle(Queries.GetCommonSettingsByCompany request, CancellationToken cancellationToken)
        {
            CommonSettingsVM oModel = new CommonSettingsVM();

            var employeeLeaveTypeQuery = $"SELECT * from leave.GetLeaveTypeByEmployee('{request.EmployeeId}')";

            var employeeLeaveTypeData = await _connection.QueryAsync<LeaveType>(employeeLeaveTypeQuery);
            oModel.LeaveTypes = employeeLeaveTypeData.ToList();

            var adminLeaveTypeQuery = $"SELECT * from leave.getleavetypebycompany('{request.CompanyId}')";

            var adminLeaveTypeData = await _connection.QueryAsync<LeaveType>(adminLeaveTypeQuery);
            //oModel.LeaveTypes = data.ToList();
            List<LeaveType> distinctLeaveTypes = adminLeaveTypeData
                          .GroupBy(p => new { p.LeaveTypeShortName, p.LeaveTypeName })
                          .Select(g => g.First())
                          .ToList();

            oModel.AdminLeaveTypes = distinctLeaveTypes;

            var queryBillType = $"SELECT * from main.GetBillTypeByCompany('{request.CompanyId}','{request.EmployeeId}')";

            var dataBillType = await _connection.QueryAsync<BillType>(queryBillType);
            oModel.BillTypes = dataBillType.ToList();
            var taskCategoryQuery = $"SELECT * from task.GetTaskCategoryById ('{request.CompanyId}')";
            var taskCategory = await _connection.QueryAsync<TaskCategory>(taskCategoryQuery).ConfigureAwait(false);
            oModel.TaskCategories = taskCategory.ToList();
            List<CommonLookup> oAttnTypes = new List<CommonLookup>();

            foreach (AttendanceRequestTypes requestTypeEnum in Enum.GetValues(typeof(AttendanceRequestTypes)))
            {
                oAttnTypes.Add(new CommonLookup { Id = (int)requestTypeEnum, Value = requestTypeEnum.ToString(), Text = requestTypeEnum.ToDescription() });
            }
            // task status

            oModel.AttendanceRequestTypes = oAttnTypes;
            List<CommonLookup> taskStatusTypes = new List<CommonLookup>();
            foreach (TaskStatuses requestTypeEnum in Enum.GetValues(typeof(TaskStatuses)))
            {
                taskStatusTypes.Add(new CommonLookup { Id = (int)requestTypeEnum, Value = requestTypeEnum.ToString(), Text = requestTypeEnum.ToDescription() });
            }
            oModel.TaskStatuses = taskStatusTypes;

            List<CommonLookup> oPunchTypes = new List<CommonLookup>();

            foreach (var name in Enum.GetNames(typeof(PunchTypes)))
            {
                oPunchTypes.Add(new CommonLookup { Id = (int)Enum.Parse(typeof(PunchTypes), name), Value = name });
            }
            oModel.PunchTypes = oPunchTypes;

            List<CommonLookup> ApprovalItems = new List<CommonLookup>();

            foreach (var name in Enum.GetNames(typeof(ApprovalStatuses)))
            {
                ApprovalItems.Add(new CommonLookup { Id = (int)Enum.Parse(typeof(ApprovalStatuses), name), Value = name });
            }
            oModel.ApprovalStatusList = ApprovalItems;

            // Half day leave type list
            List<CommonLookup> HalfDayLeaveTypeItems = new List<CommonLookup>();

            foreach (HalfDayLeaveTypes LTEnum in Enum.GetValues(typeof(HalfDayLeaveTypes)))
            {
                HalfDayLeaveTypeItems.Add(new CommonLookup { Id = (int)LTEnum, Value = LTEnum.ToString(), Text = LTEnum.ToDescription() });
            }
            oModel.HalfDayLeaveTypeList = HalfDayLeaveTypeItems;

            // Clock time type list
            List<CommonLookup> ClockTimeTyeps = new List<CommonLookup>();

            foreach (var name in Enum.GetNames(typeof(ClockTypes)))
            {
                ClockTimeTyeps.Add(new CommonLookup { Id = (int)Enum.Parse(typeof(ClockTypes), name), Value = name });
            }
            oModel.ClockTimeTypeList = ClockTimeTyeps;

            var employeeWiseCustomConfigFilter = new EmployeeWiseCustomConfigurationFilterSpecification(request.CompanyId, request.EmployeeId.Value);
            var employeeWiseCustomConfigs = await _employeeWiseCustomConfiguration.ListAsync(employeeWiseCustomConfigFilter).ConfigureAwait(false);

            //var employeeWiseCustomConfigs = await _employeeWiseCustomConfiguration.GetAll().ConfigureAwait(false);

            //oModel.EmployeeWiseCustomConfigurationList = employeeWiseCustomConfigs.ToList().FindAll(x => x.CompanyId == request.CompanyId && x.EmployeeId == request.EmployeeId);
            oModel.EmployeeWiseCustomConfigurationList = employeeWiseCustomConfigs.ToList();

            var query = new DepartmentFilterSpecification(request.CompanyId);
            var departments = await _repository.ListAsync(query).ConfigureAwait(false);
            oModel.DepartmentList = departments.ToList();

            var queryVehicleTypes = $"SELECT * from main.getcommonlookuptypebyparentcode ('{request.CompanyId}','VehicleTypes')";

            var dataVehicleTypes = await _connection.QueryAsync<CommonLookUpTypeVM>(queryVehicleTypes);
            oModel.VehicleTypeList = dataVehicleTypes.ToList();

            return oModel;
        }
    }
}