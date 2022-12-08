using ASL.Hrms.SharedKernel.Enums;
using ASL.Hrms.SharedKernel.Specifications;
using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Core.Specifications
{
    public class HolidayNotificationFilterSpecification : BaseSpecification<Notification>
    {
        public HolidayNotificationFilterSpecification()

            : base(x => x.NotificationTypeId == (short)NotificationTypes.HolidayPush && x.PushedTime.Value.Date > DateTime.Now.AddDays(-3) && x.PushedTime.Value.Date < DateTime.Now.AddDays(4))
        {

        }
    }
}