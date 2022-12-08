using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.Application.TaskDetail.Queries.Models;
using static TaskManagement.Application.TaskDetail.Commands.Commands.V1;

namespace TaskManagement.Application.TaskDetail.Commands
{
    public class TaskDetailListExcelCommandHandler : IRequestHandler<TaskDetailListExcel, List<TaskDetailListExportVM>>
    {
        #region ctor
        public TaskDetailListExcelCommandHandler(DbConnection connection, ICurrentUserContext userContext)
        {
            _userContext = userContext;
            _connection = connection;
        }

        #endregion

        #region properties
        private readonly DbConnection _connection;
        private readonly ICurrentUserContext _userContext;
        #endregion

        #region methods
        public async Task<List<TaskDetailListExportVM>> Handle(TaskDetailListExcel request, CancellationToken cancellationToken)
        {
            var query = "";
           /* if (request.CompanyId != null && request.CompanyId != Guid.Empty)
            {
                query = $"SELECT * from task. GetAllTaskDetailByCompany ('{request.CompanyId}')";
                var companyData = await _connection.QueryAsync<TaskDetailVM>(query);
                var companyDataList = companyData.ToList();
                foreach(var item  in companyDataList)
                {
                    item.Status = item.StatusId == 0 ? "In Progress" : item.StatusId == 1 ? "In queue" : "Done";
                }
                var excelTaskVm = new List<TaskDetailListExportVM>();
                excelTaskVm = companyDataList.Select(x => new TaskDetailListExportVM
                {
                   
                    AssignedBy = x.AssignedBy,
                    EndDate = x.EndDate,
                    Progress = x.Progress,
                    FullName = x.FullName,
                    TaskCategoryName = x.TaskCategoryName,
                    StartDate = x.StartDate,
                    Status = x.Status,
                    TaskDescription = x.TaskDescription,
                    TaskName = x.TaskName
                }).ToList();
                return excelTaskVm;
            }*/
            // format date
            DateTime format;
            string endDate = "", startDate = "";
            if (request.StartDate != null && request.EndDate != null)
            {
                format = (DateTime)request.EndDate;
                endDate = format.ToString("yyyy-MM-dd");
                format = (DateTime)request.StartDate;
                startDate = format.ToString("yyyy-MM-dd");
            }
            if (request.ParentTaskId == null || request.ParentTaskId == Guid.Empty)
                query = $"SELECT * from task. GetParentTaskDetail ('{request.EmployeeId}','{request.UserId}','{startDate}','{endDate}')";
            else
                query = $"SELECT * from task. GetChildTaskDetail ('{request.ParentTaskId}')";
            var data = await _connection.QueryAsync<TaskDetailVM>(query);
            var dataList = data.ToList();
            foreach (var item in dataList)
            {
                item.Status = item.StatusId == 0 ? "In Progress" : item.StatusId == 1 ? "In queue" : "Done";

                if (item.CreatedBy != null)
                {
                    query = $"SELECT * from employee.GetEmployeeInformationbyUserId ('{item.CreatedBy}')";
                    var assignedBy = await _connection.QueryFirstAsync<UserVM>(query);
                    item.AssignedBy = assignedBy.EmployeeName;
                    item.AssignedById = assignedBy.EmployeeId;


                }
                else if (item.TaskCreator != null || item.TaskCreator != Guid.Empty)
                {
                    var taskCreator = item.TaskCreator.ToString();
                    query = $"SELECT * from employee.GetEmployeeInformationbyUserId ('{taskCreator}')";
                    var assignedBy = await _connection.QueryFirstAsync<UserVM>(query);
                    item.AssignedBy = assignedBy.EmployeeName;
                    item.AssignedById = assignedBy.EmployeeId;
                }


            }
            var excelVM = new List<TaskDetailListExportVM>();
            excelVM = dataList.Select(x => new TaskDetailListExportVM
            {
                AssignedBy = x.AssignedBy,
                EndDate = x.EndDate,
                Progress = x.Progress,
                FullName = x.FullName,
                TaskCategoryName = x.TaskCategoryName,
                StartDate = x.StartDate,
                Status = x.Status,
                TaskDescription = x.TaskDescription,
                TaskName = x.TaskName
            }).ToList();
            excelVM = excelVM.OrderBy(x => x.FullName).ToList();
            return excelVM;

        }
        #endregion
    }
}
