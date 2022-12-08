using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement.Core.Interfaces
{
    public interface ILeaveSetupService
    {
        Task<bool> CheckCompanyStatus(Guid companyId);
    }
}
