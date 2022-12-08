using TaskManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ASL.Hrms.SharedKernel;

namespace TaskManagement.Core.Entities
{
    public class TaskDetailLog : BaseEntity<Guid>, IAggregateRoot
    {

        public Guid? TaskDetailId { get; private set; }
        public string UpdateInfo { get; private set; }
        public DateTime? DateUpdated { get; private set; }
        public Guid? EmployeeId { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public DateTime? SpendTime { get; private set; }
        public Boolean IsDeleted { get; set; }

        public TaskDetailLog(Guid id) : base(id) { }
        private TaskDetailLog() : base(Guid.NewGuid()) { }

        public static TaskDetailLog Create(

         Guid? taskDetailId,
         string updateInfo,
         DateTime? dateUpdated,
         Guid? employeeId,
         DateTime? startDate,
         DateTime? endDate,
         DateTime? spendTime

        )

        {
            var oModel = new TaskDetailLog(Guid.NewGuid());

            oModel.TaskDetailId = taskDetailId;
            oModel.UpdateInfo = updateInfo;
            oModel.DateUpdated = dateUpdated;
            oModel.EmployeeId = employeeId;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.SpendTime = spendTime;

            return oModel;

        }


        public void UpdateTaskDetailLog
            (

         Guid? taskDetailId,
         string updateInfo,
         DateTime? dateUpdated,
         Guid? employeeId,
         DateTime? startDate,
         DateTime? endDate,
         DateTime? spendTime

        )
        {
            TaskDetailId = taskDetailId;
            UpdateInfo = updateInfo;
            DateUpdated = dateUpdated;
            EmployeeId = employeeId;
            StartDate = startDate;
            EndDate = endDate;
            SpendTime = spendTime;
        }


        public void MarkAsDeleteTaskDetailLog()
        {
            IsDeleted = true;
        }


    }
}

