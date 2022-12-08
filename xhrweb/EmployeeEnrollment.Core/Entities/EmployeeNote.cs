using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class EmployeeNote : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public Guid? EmployeeId { get; private set; }
        public string Note { get; private set; }
        public Guid? DownloadId { get; private set; }
        public Boolean DisplayToEmployee { get; private set; }


        public EmployeeNote(Guid id) : base(id) { }
        private EmployeeNote() : base(Guid.NewGuid()) { }

        public static EmployeeNote Create(

         Guid? employeeId,
         string note,
         Guid? downloadId,
         Boolean displayToEmployee
        )

        {
            var oModel = new EmployeeNote(Guid.NewGuid());

            oModel.EmployeeId = employeeId;
            oModel.Note = note;
            oModel.DownloadId = downloadId;
            oModel.DisplayToEmployee = displayToEmployee;

            return oModel;

        }


        public void UpdateEmployeeNote
            (

         Guid? id,
         Guid? employeeId,
         string note,
         Guid? downloadId,
         Boolean displayToEmployee

        )
        {
            EmployeeId = employeeId;
            Note = note;
            DownloadId = downloadId;
            DisplayToEmployee = displayToEmployee;
        }


        //public void MarkAsDeleteEmployeeNote()
        //{
        //    IsDeleted = true;
        //}


    }
}

