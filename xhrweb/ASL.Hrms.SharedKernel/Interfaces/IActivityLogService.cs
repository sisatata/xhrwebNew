using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IActivityLogService
    {
        Task<bool> InsertActivity(string loginUserId, string currentUserCompanyId, string systemKeyword, string comment, BaseEntity<Guid> entity = null);
        Task<bool> InsertActivity(string systemKeyword, string comment, BaseEntity<Guid> entity = null);

    }
}
