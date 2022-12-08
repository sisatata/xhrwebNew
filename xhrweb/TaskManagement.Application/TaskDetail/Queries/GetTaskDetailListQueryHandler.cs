using TaskManagement.Application.TaskDetail.Queries.Models;
using Dapper;
using MediatR;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;

namespace TaskManagement.Application.TaskDetail.Queries
{

    public class GetTaskDetailListQueryHandler : IRequestHandler<Queries.GetTaskDetailList, List<TaskDetailVM>>
    {
        #region ctor
        public GetTaskDetailListQueryHandler(DbConnection connection, ICurrentUserContext userContext)
        {
            _connection = connection;
            _userContext = userContext;
        }
        #endregion

        #region properties
        private readonly DbConnection _connection;
        private readonly ICurrentUserContext _userContext;
        #endregion

        #region methods
        public async Task<List<TaskDetailVM>> Handle(Queries.GetTaskDetailList request, CancellationToken cancellationToken)
        {

            var query = "";
/*
            if (request.CompanyId != null && request.CompanyId != Guid.Empty)
            {
                query = $"SELECT * from task. GetAllTaskDetailByCompany ('{request.CompanyId}')";
                var companyData = await _connection.QueryAsync<TaskDetailVM>(query);
                var companyDataList = companyData.ToList();
                return companyDataList;
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
            dataList = dataList.OrderBy(x => x.FullName).ToList();
            return dataList;
        }
        #endregion
    }
}
