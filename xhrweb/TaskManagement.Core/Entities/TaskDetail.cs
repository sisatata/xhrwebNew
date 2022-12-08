using TaskManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;

namespace TaskManagement.Core.Entities
{
    public class TaskDetail : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? TaskTypeId { get; private set; }
        public string TaskName { get; private set; }
        public string TaskDescription { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Guid? AssigneeId { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Guid? TaskCreator { get; private set; }
        public Guid? ParentTaskId { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public int? Progress { get; private set; }

        public int StatusId { get; private set; }
        public TaskDetail(Guid id) : base(id) { }
        private TaskDetail() : base(Guid.NewGuid()) { }

        public static TaskDetail Create(
         Guid? taskTypeId,
         string taskName,
         string taskDescription,
         DateTime? startDate,
         DateTime? endDate,
         Guid? assigneeId,
         Guid? companyId,
         Guid? parentTaskId,
         int? progress,
         Guid? taskCreator
        )

        {
            var oModel = new TaskDetail(Guid.NewGuid());
            oModel.TaskTypeId = taskTypeId;
            oModel.TaskName = taskName;
            oModel.TaskDescription = taskDescription;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.AssigneeId = assigneeId;
            oModel.CompanyId = companyId;
            oModel.ParentTaskId = parentTaskId;
            oModel.IsDeleted = false;
            oModel.StatusId = 1;
            oModel.Progress = progress;
            oModel.TaskCreator = taskCreator;
            return oModel;

        }


        public void UpdateTaskDetail
            (
         Guid? taskTypeId,
         string taskName,
         string taskDescription,
         DateTime? startDate,
         DateTime? endDate,
         Guid? assigneeId,
         Guid? companyId,
         Guid? parentTaskId,
         int statusId,
         int? progress
        )
        {
            TaskTypeId = taskTypeId;
            TaskName = taskName;
            TaskDescription = taskDescription;
            StartDate = startDate;
            EndDate = endDate;
            AssigneeId = assigneeId;
            CompanyId = companyId;
            ParentTaskId = parentTaskId;
            StatusId = statusId;
            Progress = progress;
        }


        public void MarkAsDeleteTaskDetail()
        {
            IsDeleted = true;
        }


    }
}

