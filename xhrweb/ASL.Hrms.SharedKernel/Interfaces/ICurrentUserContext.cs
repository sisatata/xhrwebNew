using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface ICurrentUserContext
    {
        bool? IsAuthenticated { get; }
        string CurrentUserId { get; }
        string CurrentUserCompanyId { get; }
        string EmployeeId { get; }
        string EmployeeCode { get; }
        string EmployeeName { get; }
        
    }
}
