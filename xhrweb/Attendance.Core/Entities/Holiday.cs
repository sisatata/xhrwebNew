using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using Attendance.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace Attendance.Core.Entities
{
    public class Holiday : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        public DateTime HolidayDate { get; private set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; private set; }
        public string ReasonLocalized { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Boolean IsDeleted { get; private set; }

        public Holiday(Guid id) : base(id) { }
        private Holiday() : base(Guid.NewGuid()) { }

        public static Holiday Create(

         DateTime holidayDate,
         DateTime startDate,
         DateTime endDate,
         string reason,
         string reasonLocalized,
         Guid? companyId

        )

        {
            var oModel = new Holiday(Guid.NewGuid());

            oModel.HolidayDate = holidayDate;
            oModel.Reason = reason;
            oModel.ReasonLocalized = reasonLocalized;
            oModel.CompanyId = companyId;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;

            return oModel;

        }


        public void UpdateHoliday
            (

         DateTime holidayDate,
          DateTime startDate,
         DateTime endDate,
         string reason,
         string reasonLocalized,
         Guid? companyId

        )
        {
            HolidayDate = holidayDate;
            Reason = reason;
            ReasonLocalized = reasonLocalized;
            CompanyId = companyId;
            StartDate = startDate;
            EndDate = endDate;
        }


        public void MarkAsDeleteHoliday()
        {
            IsDeleted = true;
        }


    }
}

