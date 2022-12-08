using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IPushNotificationProcessService
    {
        Task HolidayPushNotificationProcess(Guid companyId, DateTime holidayDate);
    }
}
