using TaskManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ASL.Hrms.SharedKernel;

namespace TaskManagement.Core.Entities
{
    public class TaskCategory : BaseEntity<Guid>, IAggregateRoot
    {
        public string TaskCategoryName { get; private set; }
        public string Remarks { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; set; }

        public TaskCategory(Guid id) : base(id) { }
        private TaskCategory() : base(Guid.NewGuid()) { }

        public static TaskCategory Create(

         string taskCategoryName,
         string remarks,
         Guid? companyId

        )

        {
            var oModel = new TaskCategory(Guid.NewGuid());

            oModel.TaskCategoryName = taskCategoryName;
            oModel.Remarks = remarks;
            oModel.CompanyId = companyId;
            oModel.IsDeleted = false;
            return oModel;

        }


        public void UpdateTaskCategory
            (
         string taskCategoryName,
         string remarks,
         Guid? companyId
        )
        {

            TaskCategoryName = taskCategoryName;
            Remarks = remarks;
            CompanyId = companyId;
        }


        public void MarkAsDeleteTaskCategory()
        {
            IsDeleted = true;
        }


    }
}

