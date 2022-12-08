using ASL.Hrms.SharedKernel;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeCustomConfiguration : BaseEntity<Guid>, IAggregateRoot
    {
        public string Code { get; private set; }
        public string CustomValue { get; private set; }
        public string Description { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid EmployeeId { get; private set; }


        public EmployeeCustomConfiguration(Guid id) : base(id) { }
        private EmployeeCustomConfiguration() : base(Guid.NewGuid()) { }

        public static EmployeeCustomConfiguration Create(


         string code,
         string customValue,
         string description,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted,
         Guid employeeId

        )

        {
            var oModel = new EmployeeCustomConfiguration(Guid.NewGuid());

            oModel.Code = code;
            oModel.CustomValue = customValue;
            oModel.Description = description;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.IsDeleted = isDeleted;
            oModel.EmployeeId = employeeId;

            return oModel;

        }


        public void UpdateEmployeeCustomConfiguration
            (

         string code,
         string customValue,
         string description,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted,
         Guid employeeId

        )
        {
            Code = code;
            CustomValue = customValue;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            IsDeleted = isDeleted;
            EmployeeId = employeeId;
        }


        public void MarkAsDeleteEmployeeCustomConfiguration()
        {
            IsDeleted = true;
        }

        public void CloseEndDateEmployeeCustomConfiguration()
        {
            EndDate = DateTime.Now.AddDays(-1);
        }
    }
}

