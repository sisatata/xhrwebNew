using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.Api.Hangfire
{
    public interface IAttendanceHangfireBackgroundJobService
    {
        Task AttendanceProcessDataForAllActiveCompaniesJob();

    }
}
